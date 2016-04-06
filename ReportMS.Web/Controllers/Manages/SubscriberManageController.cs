using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    // Subscribers manage subscription informations by themselves.
    [Role]
    public class SubscriberManageController : BaseController
    {
        public ActionResult Index()
        {
            // 当前订阅者的订阅信息
            // Todo：此处是针对所有的主题，后续改为只针对于用户自订阅的主题。由管理员设置的主题应该由管理人员来操作
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
                using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
                {
                    // 删除在此 Topic 中所有的该订阅者订阅信息；当 Topic 不存在订阅者时，设置 Topic 软删除
                    service.DeleteSubscriber(topicId, email, true);
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
            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                return service.FindTopicsViaEmail(email);
            }
        }

        #endregion
    }
}