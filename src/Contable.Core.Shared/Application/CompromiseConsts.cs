using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class CompromiseConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 1000;
        public const string NameType = "VARCHAR(1000)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const string DeadlineType = "DATETIME";
        public const string Type = "INT";

        public const int PriorityReferenceMinLength = 0;
        public const int PriorityReferenceMaxLength = 50;
        public const string PriorityReferenceType = "VARCHAR(50)";

        public const string TranscriptionType = "TEXT";
        public const string FilterType = "TEXT";

        public const string IsPriorityType = "BIT";

        public const string ParameterCategoryType = "CO01";        
        public const string ParameterCategoryStatus = "CO03";
        public const string ParameterCategoryPIPPhase = "CO04";
        public const string ParameterCategoryPIPMilestone = "CO05";
        public const string CompromiseLabelIdType = "INT";
        public const string WomanCompromiseType = "BIT"; 
        public const string RecordIdType = "BIGINT";
        public const string TypeLong = "BIGINT";
        public const string CompromiseStateIdType = "INT"; 
        public const string CompromiseSubStateIdType = "INT";
    }
}
