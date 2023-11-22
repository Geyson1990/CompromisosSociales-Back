using Abp.Application.Services.Dto;
using Abp.Data;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Contable.Application;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Application.Utilities.Dto;
using Contable.Authorization.Users;
using Contable.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Contable.EntityFrameworkCore.Repositories
{
    public class ProcedureRepository : ContableRepositoryBase<User, long>, IProcedureRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public ProcedureRepository(IDbContextProvider<ContableDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider) : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public async Task<int> CallCreateRecordCodeProcess(int socialConflictId, long recordId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId),
                new SqlParameter("@RecordId", recordId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("record_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }
        public async Task<int> CallUpdateRecordCodeProcess(int socialConflictId, long recordId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId),
                new SqlParameter("@RecordId", recordId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("update_record_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateCompromiseCodeProcess(long compromiseId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompromiseId", compromiseId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("compromise_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallUpdateCompromiseCodeProcess(long compromiseId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompromiseId", compromiseId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("update_compromise_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallProspectiveRiskProcess(long userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("process_prospective_risk", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallProjectRiskProcess(long userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("process_project_risk", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSocialConflictCodeProcess(int socialConflictId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSocialConflictCodeReplaceProcess(int socialConflictId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }
        
        public async Task<int> CallSocialConflictStateProcess(int socialConflictId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_state", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallSocialConflictVerificationProccess(int socialConflictId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictId", socialConflictId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_verification", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSocialConflictAlertCodeProcess(int socialConflictAlertId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictAlertId", socialConflictAlertId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_alert_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallSocialConflictAlertStateProcess(int socialConflictAlertId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictAlertId", socialConflictAlertId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_alert_state", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSocialConflictSensibleCodeProcess(int socialConflictSensibleId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictSensibleId", socialConflictSensibleId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_sensible_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSocialConflictSensibleCodeReplaceProcess(int socialConflictSensibleId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictSensibleId", socialConflictSensibleId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_sensible_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }
       
        public async Task<int> CallSocialConflictSensibleStateProcess(int socialConflictSensibleId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictSensibleId", socialConflictSensibleId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_sensible_state", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallSocialConflictSensibleVerificationProccess(int socialConflictSensibleId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictSensibleId", socialConflictSensibleId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_sensible_verification", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateHelpMemoryCodeProcess(int helpMemoryId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@HelpMemoryId", helpMemoryId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("help_memory_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<UtilityPersonListDto>> CallTaskManagementGetAllPersons(long taskManagementId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TaskManagementId", taskManagementId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("task_management_persons", CommandType.StoredProcedure, parameters);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<UtilityPersonListDto>();

            while (reader.Read())
            {
                result.Add(new UtilityPersonListDto()
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    EmailAddress = reader.GetString("EmailAddress"),
                    Type = (PersonType)Enum.ToObject(typeof(PersonType), reader.GetInt32("Type"))
                });
            }

            return result;
        }

        public async Task<List<UtilityPersonListDto>> CallSocialConflictTaskManagementGetAllPersons(long socialConflictTaskManagementId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictTaskManagementId", socialConflictTaskManagementId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_task_management_persons", CommandType.StoredProcedure, parameters);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<UtilityPersonListDto>();

            while (reader.Read())
            {
                result.Add(new UtilityPersonListDto()
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    EmailAddress = reader.GetString("EmailAddress"),
                    Type = (PersonType)Enum.ToObject(typeof(PersonType), reader.GetInt32("Type"))
                });
            }

            return result;
        }

        public async Task<List<UtilityPersonForRecordListDto>> CallSocialConflictTaskManagementForRecordsGetAllPersons(long socialConflictTaskManagementId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SocialConflictTaskManagementId", socialConflictTaskManagementId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_task_management_persons", CommandType.StoredProcedure, parameters);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<UtilityPersonForRecordListDto>();

            while (reader.Read())
            {
                result.Add(new UtilityPersonForRecordListDto()
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    EmailAddress = reader.GetString("EmailAddress"),
                    Type = (PersonType)Enum.ToObject(typeof(PersonType), reader.GetInt32("Type")),
                    AlertSend = reader.GetBoolean("AlertSend")
                });
            }

            return result;
        }

        public async Task<PagedResultDto<UtilityConflictListGetAllDto>> CallGetAllConflictList(
            string code,
            string caseName,
            ConflictSite site,
            ConditionType lastCondition,
            int departmentId,
            int provinceId,
            int districtId,
            int skipCount,
            int maxResultCount)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Code", code),
                new SqlParameter("@Name", caseName),                
                new SqlParameter("@Site", site),
                new SqlParameter("@LastCondition", lastCondition),
                new SqlParameter("@Deparment", departmentId),
                new SqlParameter("@Province", provinceId),
                new SqlParameter("@District", districtId),
                new SqlParameter("@SkipCount", skipCount),
                new SqlParameter("@MaxResultCount", maxResultCount)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_help_memory_list", CommandType.StoredProcedure, parameters);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<UtilityConflictListGetAllDto>();
            var count = 0;

            while (reader.Read())
            {
                count = reader.GetInt32("Count");

                result.Add(new UtilityConflictListGetAllDto()
                {
                    Id = reader.GetInt32("Id"),
                    Code = reader.GetString("Code"),
                    Name = reader.GetString("Name"),
                    TerritorialUnits = reader.GetString("TerritorialUnits"),
                    Site = (ConflictSite)Enum.ToObject(typeof(ConflictSite), reader.GetInt32("Type")),
                    LastCondition = (ConditionType)Enum.ToObject(typeof(ConditionType), reader.GetInt32("LastCondition"))
                });
            }

            return new PagedResultDto<UtilityConflictListGetAllDto>(count, result);
        }

        public async Task<PagedResultDto<SocialConflictTaskManagementConflictGetAllDto>> CallGetAllConflictTaskManagements(
            string names, 
            string codes,
            DateTime? startDate, 
            DateTime? endDate,
            ConflictSite site,
            int skipCount, 
            int maxResultCount)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", names),
                new SqlParameter("@Code", codes),
                new SqlParameter("@Site", site),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate),
                new SqlParameter("@SkipCount", skipCount),
                new SqlParameter("@MaxResultCount", maxResultCount)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("social_conflict_task_management_list", CommandType.StoredProcedure, parameters);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<SocialConflictTaskManagementConflictGetAllDto>();
            var count = 0;

            while (reader.Read())
            {
                count = reader.GetInt32("Count");

                result.Add(new SocialConflictTaskManagementConflictGetAllDto()
                {
                    Id = reader.GetInt32("Id"),
                    CreationTime = reader.GetDateTime("CreationTime"),
                    Code = reader.GetString("Code"),
                    Name = reader.GetString("Name"),
                    Tasks = reader.GetInt32("Tasks"),
                    Type = (ConflictSite)Enum.ToObject(typeof(ConflictSite), reader.GetInt32("Type"))
                });
            }

            return new PagedResultDto<SocialConflictTaskManagementConflictGetAllDto>(count, result);
        }

        public async Task<int> CallCreateInterventionPlanCodeProcess(int interventionPlanId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@InterventionPlanId", interventionPlanId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("intervention_plan_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateInterventionPlanCodeReplaceProcess(int interventionPlanId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@InterventionPlanId", interventionPlanId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("intervention_plan_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateInterventionPlanStateProcess(int interventionPlanId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@InterventionPlanId", interventionPlanId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("intervention_plan_state", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateCrisisCommitteeCodeProcess(int crisisCommitteenId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CrisisCommitteeId", crisisCommitteenId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("crisis_committee_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateCrisisCommitteeCodeReplaceProcess(int crisisCommitteenId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CrisisCommitteeId", crisisCommitteenId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("crisis_committee_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSectorMeetCodeProcess(int sectorMeetId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SectorMeetId", sectorMeetId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("sector_meet_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateSectorMeetCodeReplaceProcess(int sectorMeetId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SectorMeetId", sectorMeetId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("sector_meet_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateDialogSpaceCodeProcess(int dialogSpaceId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DialogSpaceId", dialogSpaceId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("dialog_space_code", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateDialogSpaceCodeReplaceProcess(int dialogSpaceId, int year, int count)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DialogSpaceId", dialogSpaceId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Count", count)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("dialog_space_code_replace", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallCreateDialogSpaceStateProcess(int dialogSpaceId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DialogSpaceId", dialogSpaceId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("dialog_space_state", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.CommandTimeout = 60;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private async Task EnsureConnectionOpenAsync()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(ContableDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }

        public async Task<int> CallGenerateMeetProcess(int sectorMeetId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SectorMeetId", sectorMeetId)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("generate_meet", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CallGenerateSendAlert(int idPerson)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonId", idPerson)
            };

            await EnsureConnectionOpenAsync();

            using var command = CreateCommand("send_alert", CommandType.StoredProcedure, parameters);

            return await command.ExecuteNonQueryAsync();
        }
    }
}