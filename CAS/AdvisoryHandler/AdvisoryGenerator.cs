namespace CAS.AdvisoryHandler
{
    public static class AdvisoryGenerator
    {
        public static string GetWateringAdvice(string crop, string season)
        {
            if (season == "Rainy")
                return "Water only when rainfall is insufficient. Avoid waterlogging.";

            return "Irrigate every 5–7 days depending on soil moisture.";
        }

        public static string GetFertilizerAdvice(string crop)
        {
            return crop switch
            {
                "Maize" => "Apply NPK 15-15-15 two weeks after planting and top-dress with Urea after six weeks.",
                "Rice" => "Apply NPK during transplanting and Urea during tillering.",
                "Cassava" => "Apply NPK 12-12-17 about one month after planting.",
                "Tomato" => "Apply compost before planting and NPK during flowering.",
                _ => "Apply fertilizer according to soil test recommendations."
            };
        }

        public static string GetPestControlAdvice(string crop)
        {
            return crop switch
            {
                "Maize" => "Scout weekly for Fall Armyworm and apply approved pesticides when infestation is observed.",
                "Rice" => "Monitor for stem borers and rice blast disease.",
                "Tomato" => "Monitor for whiteflies and fungal diseases. Apply approved fungicides when necessary.",
                "Pepper" => "Inspect regularly for aphids, thrips and whiteflies.",
                _ => "Inspect crops weekly and control pests early using approved pesticides."
            };
        }

        public static string GetHarvestingTips(string crop)
        {
            return crop switch
            {
                "Maize" => "Harvest when husks are dry and grains are hard.",
                "Rice" => "Harvest when about 85% of the grains turn golden yellow.",
                "Cassava" => "Harvest between 9–12 months after planting depending on variety.",
                "Tomato" => "Harvest fruits at the breaker or fully ripe stage.",
                "Pepper" => "Harvest mature fruits regularly to encourage more production.",
                _ => "Harvest only when crops reach full maturity."
            };
        }
    }
}
