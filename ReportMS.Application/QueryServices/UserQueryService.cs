using System.Text;
using Gear.Infrastructure.Storage;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.QueryServices
{
    public class UserQueryService : IUserQueryService
    {
        #region IUserQueryService Members

        public UserDto Find(string userName)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine("SELECT ");
            sqlQuery.AppendLine("   EMAIL_ADDR AS UserName ");
            sqlQuery.AppendLine("   ,PASSWORD AS Password ");
            sqlQuery.AppendLine("   ,EMPLR_ID AS EmployeeNo ");
            sqlQuery.AppendLine("   ,EMAIL_ADDR	AS Email ");
            sqlQuery.AppendLine("   ,ENG_NAME AS EnglishName ");
            sqlQuery.AppendLine("   ,LOCAL_NAME AS LocalName ");
            sqlQuery.AppendLine("   ,ORG_NAME AS Organization ");
            sqlQuery.AppendLine("   ,ORG_DESC AS OrganizationDescription ");
            sqlQuery.AppendLine("   ,DIV_NAME AS Department ");
            sqlQuery.AppendLine("   ,JOBTITLE_NAME AS Job ");
            sqlQuery.AppendLine("   ,CELL_PH_NUM AS Tel ");
            sqlQuery.AppendLine("   ,EXTENSION AS Extension ");
            sqlQuery.AppendLine("   ,VOIP ");
            sqlQuery.AppendLine("   ,manager AS Manager ");
            sqlQuery.AppendLine(" FROM VW_people_finder ");
            sqlQuery.AppendLine(" WHERE EMAIL_ADDR = @UserName ");

            return StorageManager.CreateInstance(StorageSettings.Ez)
                .SelectFirstOrDefault<UserDto>(sqlQuery.ToString(), new {UserName = userName});
        }

        #endregion
    }
}
