using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gear.Infrastructure;
using Gear.Infrastructure.Net.Mail;
using Gear.Infrastructure.Web.Utility;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    public class TopicManageController : BaseController
    {
        [Role]
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
            {
                var model = service.FindAll();
                return View(model);
            }
        }

        // 由订阅主题拥有者来索引
        public ActionResult Owner()
        {
            var owner = this.LoginUser.Identity.Name;
            using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
            {
                // Find topics by creator (owner)
                var model = service.FindTopicsViaOwner(owner);
                return View("Index", model);
            }
        }

        public ActionResult Edit(Guid topicId)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
            {
                var model = service.Find(topicId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(TopicDto model)
        {
            var subscribers = this.GetValidSubscribers();
            if (subscribers == null || !subscribers.Any())
                return Json(false, "Modify the topic failure. there are not any subscriber.");

            model.Subscribers = (from subscriber in subscribers
                select new SubscriberDto
                {
                    Email = subscriber
                }).ToList();

            model.UpdatedBy = this.LoginUser.Identity.Name;
            this.EncodeInput(model);

            try
            {
                using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
                {
                    service.ModifyTopic(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Modify the topic failure.");
            }

            return Json(true);
        }

        [HttpPost]
        public ActionResult Delete(Guid topicId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
                {
                    service.RemoveTopic(topicId, this.LoginUser.Identity.Name);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the topic failure.");                
            }
            
            return Json(true);
        }

        #region Private Methods

        private void EncodeInput(TopicDto topic)
        {
            topic.Body = HttpUtility.HtmlEncode(topic.Body);
        }

        private string[] GetValidSubscribers()
        {
            var subscriberKey = "subscribers";
            var subscribers = HttpRequestHelper.GetVariableFromQueryStringOrForm(subscriberKey);
            if (String.IsNullOrWhiteSpace(subscribers))
                return null;

            return (from subscriber in subscribers.Split(',', ';')
                    where MailAudit.ValidateRecipients(subscriber)
                    select subscriber).ToArray();
        }

        #endregion
    }
}