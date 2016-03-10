using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Reports.Managers;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Helpers;

namespace ReportMS.Web.Controllers
{
    public class SubscriberController : BaseController
    {
        private const string ReportId = "__tableOrViewId";
        private const string TableOrView = "__tableOrView";
        private const string Sql = "__sql";
        private const string SqlParameter = "__parameters";

        public void UserSubscribeStore()
        {
            var report = ServiceLocator.Instance.Resolve<IReportRead>();
            var sqlstatement = report.GetSqlQueryAndParameters();

            TempData[ReportId] = report.ReportId;
            TempData[TableOrView] = report.TableOrViewName;
            TempData[Sql] = sqlstatement.Item1;
            TempData[SqlParameter] = sqlstatement.Item2;
        }

        public ActionResult UserSubscribe()
        {
            if (!TempData.ContainsKey(ReportId))
                return Content("There are not report or any fields to subscribe.");

            var reportId = (Guid) TempData[ReportId];
            var tableOrView = TempData[TableOrView] as string;
            var sql = TempData[Sql] as string;
            var sqlParameter = TempData[SqlParameter] as IDictionary<string, object>;
            var parameters = StorageParameter.ConvertParameterToString(sqlParameter);
            var subscribers = new List<SubscriberDto> {new SubscriberDto {Email = this.LoginUser.Email}};

            var model = new AttachmentTopicDto
            {
                TopicName = tableOrView,
                ReportId = reportId,
                SqlStatement = sql,
                Parameter = parameters,
                Subscribers = subscribers
            };

            this.ViewBagTask();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserSubscribe(AttachmentTopicDto model)
        {
            var subscribers = new List<SubscriberDto> { new SubscriberDto { Email = this.LoginUser.Email } };
            model.Subscribers = subscribers;
            model.CreatedBy = this.LoginUser.Identity.Name;

            try
            {
                using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
                {
                    service.CreateAttachmentTopic(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the topic failure.");
            }

            return Json(true);
        }

        #region

        public ActionResult SubscribeSchedule(TaskScheduleDto schedule)
        {
            var views = new Dictionary<TaskScheduleDto, string>
            {
                {TaskScheduleDto.Yearly, "Tuple/_YearSchedule"},
                {TaskScheduleDto.Monthly, "Tuple/_MonthSchedule"},
                {TaskScheduleDto.Daily, "Tuple/_DaySchedule"},
                {TaskScheduleDto.Weekly, "Tuple/_WeekSchedule"},
                {TaskScheduleDto.Hourly, "Tuple/_HourSchedule"}
            };
            var view = views.First(v => v.Key == schedule).Value;

            this.ViewBagSchedule();
            return PartialView(view);
        }

        private void ViewBagSchedule()
        {
            var monthRange = Enumerable.Range(1, 12)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = s.ToString()});

            // DayOfWeek.Sunday -- 0
            var weekRange = Enumerable.Range((int) DayOfWeek.Sunday, 7)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = ((DayOfWeek) s).ToString()});

            var dayRange = Enumerable.Range(1, 31)
                .Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() });

            var hourRange = Enumerable.Range(1, 23)
                .Select(s => new SelectListItem { Value = s.ToString(), Text = string.Format("{0}:00", s.ToString()) });

            ViewBag.MonthRange = monthRange;
            ViewBag.WeekRange = weekRange;
            ViewBag.DayRange = dayRange;
            ViewBag.HourRange = hourRange;
        }

        private void ViewBagTask()
        {
            var first = Enumerable.Range(1, 1)
                .Select(s => new SelectListItem { Value = "", Text = "-----" });

            // TaskScheduleDto.Hourly -- 1
            // TaskScheduleDto.Yearly -- 5
            var scheduleRange = Enumerable.Range(1, 5)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = ((TaskScheduleDto) s).ToString()});

            ViewBag.ScheduleRange = first.Union(scheduleRange);
        }

        #endregion
    }
}