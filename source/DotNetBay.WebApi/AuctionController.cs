using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetBay.WebApi.DTOs;
using DotNetBay.Data.EF;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using DotNetBay.Core.Execution;
using DotNetBay.Core;

namespace DotNetBay.WebApi
{
    [RoutePrefix("api")]
    public class AuctionController : ApiController
    {
        //private static ;
        public static EFMainRepository repo { get; private set; }
        public static IAuctionRunner AuctionRunner { get; private set; }
        private static IMemberService MemberService;
        private static AuctionService AuctionService;

        static AuctionController()
        {
            repo = new EFMainRepository();
            MemberService = new SimpleMemberService(repo);
            AuctionService = new AuctionService(repo, MemberService);
            AuctionRunner = new AuctionRunner(repo);
            AuctionRunner.Start();
        }

        public AuctionController()
        {
            //repo = new EFMainRepository();
        }

        [Route("auctions/{id:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetAuction(long id)
        {
            var ra = AuctionService.GetById(id);
            if (ra == null)
            {
                return NotFound();
            }

            Auction a = new Auction(ra);
            return Ok(a);
        }

        [Route("auctions")]
        [HttpGet]
        public IEnumerable<AllAuctionsAuction> GetAuctions()
        {
            List<AllAuctionsAuction> all = new List<AllAuctionsAuction>();
            AuctionService.GetAll().ToList().ForEach(a => all.Add(new AllAuctionsAuction(a)));

            return all;
        }

        [HttpGet]
        [Route("auctions/{id:min(1)}/image")]
        public HttpResponseMessage Image(int id)
        {
            var auction = AuctionService.GetById(id);

            if (auction == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if (auction.Image != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(auction.Image);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return result;
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("auctions/{id:min(1)}/image")]
        public async Task<IHttpActionResult> SetAuctionImage(long id)
        {
            var auction = AuctionService.GetById(id);
            if (auction == null) return NotFound();

            var streamProvider = await Request.Content.ReadAsMultipartAsync();
            foreach (var file in streamProvider.Contents)
            {
                var imageByteArray = await file.ReadAsByteArrayAsync();
                auction.Image = imageByteArray;
                AuctionService.Save(auction); 
            }
            return Ok();
        }

        [HttpPost]
        [Route("auctions")]
        public IHttpActionResult CreateAuction([FromBody] Auction auction)
        {
            var seller = MemberService.GetAll().FirstOrDefault(m => m.DisplayName == auction.Seller); 

            if (seller == null)
            {
                seller = MemberService.Add(auction.Seller, $"{auction.Seller}@WebAPI.com");
            }

            var a = AuctionService.Save(new Data.Entity.Auction()
            {
                Title = auction.Title,
                Description = auction.Description,
                StartPrice = auction.StartPrice,
                StartDateTimeUtc = auction.StartDateTimeUtc,
                EndDateTimeUtc = auction.EndDateTimeUtc,
                Seller = seller
            });

            return Ok(a.Id.ToString());
        }

        [HttpPost]
        [Route("auctions/{id:min(1)}/bid")]
        public IHttpActionResult AddBid(long id, [FromBody] Bid bid)
        {
            var auction = AuctionService.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }

            var bidder = MemberService.GetAll().FirstOrDefault(m => m.DisplayName == bid.Bidder);  

            if (bidder == null)
            {
                bidder = MemberService.Add(bid.Bidder, $"{bid.Bidder}@WebAPI.com");
            }

            // how to set bidder??
            var b = AuctionService.PlaceBid(auction, bid.Amount);

            return Ok(b.Id.ToString());
        }
    }
}
