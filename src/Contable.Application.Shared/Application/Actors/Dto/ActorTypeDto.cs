using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace Contable.Application.Actors.Dto
{
    public class ActorTypeDto : EntityDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
        public bool ShowDetail { get; set; }
        public bool ShowMovement { get; set; }
    }
}