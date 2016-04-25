using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gear.Infrastructure;
using Gear.Infrastructure.Web.Attributes;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers.Manages
{
    // Subscribers manage subscription informations by themselves.
    [AllowAuthenticated]
    public class SubscriberManageController : BaseController
    {
        public ActionResult Index()
        {
            // 当前订阅者的订阅信息
            var email = this.LoginUser.Email;
            var topics = this.GetTopics(email);
            return View(topics);
        }

        [HttpPost]
        public ActionResult DeleteSubscriber(Guid topicId)
        {
            var email = this.LoginUser.Email;

            try
            {
                using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
                {
                    // 删除在此 Topic 中所有的该订阅者订阅信息
                    service.DeleteSubscriber(topicId, email);
                }
            }
            catch (Exception)
            {
                return Json(false, "Remove the subscriber failure.");
            }

            return Json(true);
        }

        #region Private Methods

        private IEnumerable<TopicDto> GetTopics(string email)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
            {
                return service.FindTopicsViaEmail(email);
            }
        }

        #endregion
    }
}