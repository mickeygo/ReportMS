using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.Web.Client.Options
{
    /// <summary>
    /// 订阅者选项
    /// </summary>
    public static class SubscriberOptions
    {
        /// <summary>
        /// Month 月选项: 1 ~ 12 月
        /// </summary>
        /// <returns>SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> Month()
        {
            // Month: 1 ~ 12 月
            return Enumerable.Range(1, 12)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = s.ToString()});
        }

        /// <summary>
        /// DayOfWeek 选项: 0 ~ 6
        /// </summary>
        /// <returns>SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> DayInWeek()
        {
            // DayOfWeek.Sunday -- 0
            return Enumerable.Range((int) DayOfWeek.Sunday, 7)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = ((DayOfWeek) s).ToString()});
        }

        /// <summary>
        /// Day: 1 ~ 31
        /// </summary>
        /// <returns>SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> Day()
        {
            return Enumerable.Range(1, 31)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = s.ToString()});
        }

        /// <summary>
        /// Hour: 1:00 ~ 23:00，不包含 24(0:00) 点
        /// </summary>
        /// <returns>SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> Hour()
        {
            return Enumerable.Range(1, 23)
                .Select(s => new SelectListItem {Value = s.ToString(), Text = string.Format("{0}:00", s.ToString())});
        }

        /// <summary>
        /// TaskSchedule 选项， Hourly ~ Yearly
        /// </summary>
        /// <returns>SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> Schedule()
        {
            // TaskScheduleDto.Hourly -- 1
            // TaskScheduleDto.Yearly -- 5
            return Enumerable.Range(1, 5)
                .Select(s => new SelectListItem { Value = s.ToString(), Text = ((TaskScheduleDto)s).ToString() });
        }
    }
}
