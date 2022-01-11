using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ToDoDto
    {
        /// <summary>
        /// Maps to the id property of the entity. Ignored for new entries.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Maps to the Title property of the entity.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Maps to the isComplete property of the entity. Ignored for new entries.
        /// </summary>
        [JsonProperty("isComplete", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsComplete { get; set; }

        /// <summary>
        /// Maps to the createdOn property of the entity. Ignored for new entries.
        /// </summary>
        [JsonProperty("createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }
    }
}
