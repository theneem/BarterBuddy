using System.Threading.Tasks;
using BarterBuddy.Common.Helper;
using BarterBuddy.Data.BarterBuddyEDMX;
using BarterBuddy.Data.IData;
using BarterBuddy.Model;

namespace BarterBuddy.Data
{
    public class LoginDataRepository : ILoginDataRepository
    {
        public LoginDataRepository()
        {

        }

        public async Task<ResponseHelper> RegisterUser(BBUser userModel)
        {
            ResponseHelper helper = new ResponseHelper { StatusCode = Enums.ResponseCode.Success };
            using (var db = new barterbuddyEntities())
            {
                db.BBUsers.Add(userModel);
                db.SaveChanges();
                helper.Payload = userModel.UserID;
            }

            return helper;
        }
    }
}
