using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace AnimeScrapperLibrary;

public class AnimeWebScrapper
{
    private readonly string _url = "https://www.imdb.com/list/ls525603926/";
    public List<AnimeScrapped> AnimeList {get; set;} = new ();
    
    public void Load()
    {
        var html = new HtmlWeb();
        HtmlDocument document = html.Load(_url);
        var animeHTMLelements = document.DocumentNode.QuerySelectorAll("li.ipc-metadata-list-summary-item");


        for(int i = 0; i < 10; i++)
        {
            var anime =  animeHTMLelements.ElementAt(i);
            List<HtmlNode> metadataCollection = anime.QuerySelectorAll("span.dli-title-metadata-item").ToList();
            AnimeList.Add(new AnimeScrapped() 
            {
                Title = HtmlEntity.DeEntitize(anime.QuerySelector("h3").InnerText).Substring(3).Trim(),
                ImageURL = HtmlEntity.DeEntitize(anime.QuerySelector("img").Attributes["src"].Value),
                Metadata = string.Join("  --  ", metadataCollection.Select(element => element.InnerText)),
                Rating = double.Parse(anime.QuerySelector("span.ipc-rating-star--rating").InnerText),
                Description = HtmlEntity.DeEntitize(anime.QuerySelector("div.ipc-html-content-inner-div").InnerText),
            });
        }
    }
}
