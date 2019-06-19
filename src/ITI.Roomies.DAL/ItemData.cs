namespace ITI.Roomies.DAL
{
    public class ItemData
    {
        public int ItemId { get; set; }

        public int ItemPrice {get; set;}

        public string ItemName { get; set; }

        public string ItemQuantite { get; set; }

        public bool ItemBought { get; set; }

        public int CourseId { get; set;}

        public int RoomieId { get; set; }

        public bool IsRepeated { get; set; }
    }
}
