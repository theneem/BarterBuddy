using System;
using System.Linq;
using System.Threading.Tasks;
using BarterBuddy.Common.Helper;
using BarterBuddy.Data.BarterBuddyEDMX;
using BarterBuddy.Data.IData;
using BarterBuddy.Model;
using BarterBuddy.Resources.CommonResource;

namespace BarterBuddy.Data
{
    public class LoginDataRepository : ILoginDataRepository
    {
        public LoginDataRepository()
        {

        }

        public async Task<ResponseHelper> GetUserDetail(UserModel userModel)
        {

            ResponseHelper helper = new ResponseHelper { StatusCode = Enums.ResponseCode.Success };
            try
            {
                using (var db = new BarterBuddyContext())
                {

                    helper.Payload = db.BBUsers.FirstOrDefault(t => t.UserID == userModel.UserID);
                }
            }
            catch (Exception ex)
            {
                helper.StatusCode = Enums.ResponseCode.Error;
                helper.Message = ex.Message.ToString();
            }

            return helper;
        }

        public async Task<ResponseHelper> RegisterUserModel(UserRegisterModel userModel)
        {
            ResponseHelper helper = new ResponseHelper { StatusCode = Enums.ResponseCode.Success };
            try
            {
                using (var db = new BarterBuddyContext())
                {

                    var userEmailIdExists = db.BBUsers.Any(t => t.UserName == userModel.EmailId);
                    if (userEmailIdExists)
                    {
                        helper.StatusCode = Enums.ResponseCode.Error;
                        helper.Message = CommonResource.EmailAlreadyExist;
                        return helper;
                    }
                    var userMobileExists = db.BBUserDetails.Any(t => t.WhatsAppNumber == userModel.MobileNo);
                    if (userEmailIdExists)
                    {
                        helper.StatusCode = Enums.ResponseCode.Error;
                        helper.Message = CommonResource.MobileAlreadyExist;
                        return helper;
                    }

                    BBUser newUser = new BBUser
                    {
                        UserName = userModel.EmailId,
                        Password = userModel.Password,
                        LoginType = Enums.UserType.SGEAdmin.GetHashCode(),
                        CreatedBy = Enums.LoginPlatForm.System.ToString(),
                        ModifiedBy = Enums.LoginPlatForm.System.ToString(),
                        CreatedDate = DateTime.Now,
                        ModifidDate = DateTime.Now
                    };

                    db.BBUsers.Add(newUser);
                    db.SaveChanges();
                    BBUserDetail newUserDetail = new BBUserDetail
                    {
                        UserID = newUser.UserID,
                        Name = userModel.EmailId,
                        WhatsAppNumber = userModel.MobileNo
                    };

                    db.BBUserDetails.Add(newUserDetail);
                    db.SaveChanges();
                    helper.Payload = newUserDetail.UserDetailID;
                }
            }
            catch (Exception ex)
            {
                helper.StatusCode = Enums.ResponseCode.Error;
                helper.Message = ex.Message.ToString();
            }

            return helper;
        }

        public async Task<ResponseHelper> UpdateProfile(UserModel userModel)
        {
            ResponseHelper helper = new ResponseHelper { StatusCode = Enums.ResponseCode.Success };
            try
            {
                using (var db = new BarterBuddyContext())
                {
                    var userObject = db.BBUsers.First(t => t.UserID == userModel.UserID);

                    userObject.UserName = userModel.UserName;
                    userObject.Password = userModel.Password;
                    userObject.LoginType = userModel.LoginType;
                    userObject.ModifiedBy = userModel.CreatedBy;
                    userObject.ModifidDate = userModel.CreatedDate;
                    db.BBUsers.Attach(userObject);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                helper.StatusCode = Enums.ResponseCode.Error;
                helper.Message = ex.Message.ToString();
            }

            return helper;
        }

        public async Task<ResponseHelper> ValidateUser(UserModel userModel)
        {
            ResponseHelper helper = new ResponseHelper { StatusCode = Enums.ResponseCode.Success };
            try
            {
                using (var db = new BarterBuddyContext())
                {
                    var isExists = db.BBUsers.Any(t => t.UserName == userModel.UserName && t.Password == userModel.Password);
                    if (!isExists)
                    {
                        helper.StatusCode = Enums.ResponseCode.Error;
                        helper.Message = CommonResource.InvalidUserPassword;
                    }
                    else
                    {
                        var user = db.BBUsers.First(t => t.UserName == userModel.UserName && t.Password == userModel.Password);
                        if (user != null)
                        {
                            helper.Payload = new UserModel
                            {
                                UserID = user.UserID,
                                UserName = user.UserName,
                                Password = user.Password,
                                LoginType = user.LoginType,
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                helper.StatusCode = Enums.ResponseCode.Error;
                helper.Message = ex.Message.ToString();
            }

            return helper;
        }
    }
}
