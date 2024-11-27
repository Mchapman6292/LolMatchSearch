using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;


namespace LolMatchFilterNew.Domain.DTOs.LeaguepediaMatchDTOs
{
    public class LeaguepediaMatchDTO
    {

        public string LeaguepediaGameId { get; set; }
        public string DateTime { get; set; }
        public string Tournament { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Winner { get; set; }

        public ICollection<Processed_ProPlayerEntity> Team1Players { get; set; }

        public virtual ICollection<Processed_ProPlayerEntity> Team2Players { get; set; }



    }
}
