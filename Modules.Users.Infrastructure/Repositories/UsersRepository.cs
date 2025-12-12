using Common.SharedClasses.Enums;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MimeKit.Text;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Entities.Auth;
using Modules.Users.Domain.Entities.Devices;
using Modules.Users.Domain.Repositories;
using Modules.Users.Infrastructure.Persistence;

namespace Modules.Users.Infrastructure.Repositories
{
    internal class UsersRepository(UserDbContext userDbContext, IConfiguration configuration,
        UserManager<User> userManager, ITokenRepository tokenRepository,
        IHostEnvironment hostEnvironment, IDeviceRepository deviceRepository,
        IOTPRepository otpRepository) : GenericRepository<User>(userDbContext), IUsersRepository
    {
        public async Task<User> GetUserAsync(string id)
        {

            return await userDbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
        public async Task<User> FindUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<User> FindUserByUserName(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }
        public async Task<User> GetUserWithDevicesAsync(string id)
        {
            return await userDbContext.Users.Include(u => u.Devices).FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        //public async Task<bool> FillWallet(string email, int amount)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    user.Wallet += amount;
        //    await userManager.UpdateAsync(user);
        //    await dbcontext.SaveChangesAsync();
        //    return true;
        //}

        public async Task<IEnumerable<IdentityError>> Register(User owner, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(owner.Email);
            if (user != null)
            {
                var list = new List<IdentityError>
            {
                new()
                {
                    Code = "User already exists",
                    Description = "User with the same email already exists"
                }
            };
            }
            var res = await userManager.CreateAsync(owner, password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(owner, role);
                if (role.ToUpper() == nameof(EnumRoleNames.User).ToUpper())
                {
                    var code = new Random().Next(100000, 999999).ToString();
                    await SendEmail(owner.Email, code);
                    await otpRepository.AddAsync(new OTP()
                    {
                        Code = code,
                        UserId = owner.Id,
                        Consumed = false,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(10)
                    });
                }
                else
                {
                    owner.EmailConfirmed = true;
                    await userManager.UpdateAsync(owner);
                }

            }

            return res.Errors;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password)
        {
            var check = await userManager.CreateAsync(user, password);
            if (check.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
            return check.Errors;
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(User user, string password, string verifyUrl)
        {
            if (await userManager.FindByEmailAsync(user.Email) != null)
            {
                var list = new List<IdentityError>
            {
                new()
                {
                    Code = "User already exists",
                    Description = "User with the same email already exists"
                }
            };
                return list;
                //throw new UserAlreadyExistsException(user.Email);
            }

            var check = await userManager.CreateAsync(user, password);

            if (check.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                var code = new Random().Next(100000, 999999).ToString();
                await SendEmail(user.Email, code);
                await otpRepository.AddAsync(new OTP()
                {
                    Code = code,
                    UserId = user.Id,
                    Consumed = false,
                    ExpiresAt = DateTime.UtcNow.AddHours(2)
                });
                await userDbContext.SaveChangesAsync();
            }
            return check.Errors;
        }
        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return false;
            }
            var otp = await otpRepository.GetOtpFromCode(code, user.Id);
            if (otp == null || otp.Consumed == true || DateTime.UtcNow > otp.ExpiresAt)
            {
                return false;
            }
            user.EmailConfirmed = true;
            otp.Consumed = true;
            await userDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> NumberOfUsersInRole(string roleId)
        {
            var num = await userDbContext.UserRoles.Where(ur => ur.RoleId == roleId).CountAsync();
            return num;
        }

        public async Task<int> NewUsersAfterMonth(int month, string roleId, int year)
        {
            var records = from ur in userDbContext.UserRoles
                          join u in userDbContext.Users
                          on ur.UserId equals u.Id
                          where u.CreatedAt.Month == month
                          where u.CreatedAt.Year == year
                          where ur.RoleId == roleId
                          select ur;

            var num = await records.CountAsync();
            return num;
        }
        public async Task<bool> CheckPassword(string userId, string password)
        {
            var user = await GetUserAsync(userId);
            if (user == null || password == null)
            {
                return false;
            }
            var sucess = await userManager.CheckPasswordAsync(user, password);
            return sucess;
        }

        public async Task UpdateUser(User user)
        {
            await userManager.UpdateAsync(user);
            await userDbContext.SaveChangesAsync();
        }

        //public async Task DeleteAccount(string userId)
        //{
        //    var user = await userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        //throw new NotFoundException(nameof(User), userId);
        //        throw new InvalidOperationException("User not found");
        //    }

        //    user.IsDeleted = true;
        //    await userManager.UpdateSecurityStampAsync(user);
        //    await userManager.UpdateAsync(user);
        //    await dbContext.SaveChangesAsync();
        //}
        public async Task SendEmail(string userEmail, string code)
        {
            var emailMessage = new MimeMessage();
            //for etherreal put "darlene.mcdermott@ethereal.email";
            emailMessage.From.Add(MailboxAddress.Parse("darlene.mcdermott@ethereal.email"));
            emailMessage.To.Add(MailboxAddress.Parse(userEmail));
            emailMessage.Subject = "Email Confirmation OTP";
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = $"Your Email Confirmation code is: {code}"
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);

            //  smtp.Authenticate("darlene.mcdermott@ethereal.email", "9q8QFbScP9VKDBg6cx");
            smtp.Authenticate("darlene.mcdermott@ethereal.email", "9q8QFbScP9VKDBg6cx");
            smtp.Send(emailMessage);
            await smtp.DisconnectAsync(true);
            return;
        }

        public async Task<string> SaveUserProfileAsync(IFormFile userImage)
        {
            if (userImage == null)
                return null;

            var contentPath = hostEnvironment.ContentRootPath;
            var specialPath = "Images/Users";
            var path = Path.Combine(contentPath, specialPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(userImage.FileName);
            var imageName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, imageName);
            var returnName = Path.Combine(specialPath, imageName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await userImage.CopyToAsync(stream);
            return returnName;
        }
        //public bool DeleteImage(string imageName)
        //{
        //    if (imageName == null)
        //    {
        //        return false;
        //    }
        //    if (!File.Exists(imageName))
        //    {
        //        return false;
        //    }
        //    File.Delete(imageName);
        //    return true;
        //}
        //public async Task<bool> UpdateUserImage(string userId, IFormFile newImage)
        //{
        //    if (userId == null || newImage == null)
        //    {
        //        return false;
        //    }
        //    var user = await userManager.FindByIdAsync(userId);
        //    if (user.ProfileImagePath != null)
        //    {
        //        var success = DeleteImage(user.ProfileImagePath);
        //    }
        //    var newImagePath = await SaveUserProfileAsync(newImage);
        //    user.ProfileImagePath = newImagePath;
        //    await userManager.UpdateAsync(user);
        //    await dbcontext.SaveChangesAsync();
        //    return true;
        //}
        public async Task<AuthResponse>? LoginUser(string email, string password, string deviceToken)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            bool isValidCredentials = await userManager.CheckPasswordAsync(user, password);
            bool isConfirmedEmail = await userManager.IsEmailConfirmedAsync(user);
            if (isValidCredentials && isConfirmedEmail)
            {
                var existingDevice = await deviceRepository.GetDeviceByToken(deviceToken, user.Id);
                if (existingDevice != null)
                {
                    existingDevice.LastLoggedInAt = DateTime.UtcNow;
                    await deviceRepository.SaveChangesAsync();
                }
                else
                {

                    var device = new Device()
                    {
                        LastLoggedInAt = DateTime.UtcNow,
                        FcmToken = deviceToken,
                        UserId = user.Id,
                        IsActive = true
                    };
                    await deviceRepository.AddAsync(device);
                }
                var token = await tokenRepository.GenerateToken(user.Id);
                return token;
            }
            return null;
        }
        public async Task<AuthResponse>? LoginUserWithoutDevice(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            bool isValidCredentials = await userManager.CheckPasswordAsync(user, password);
            bool isConfirmedEmail = await userManager.IsEmailConfirmedAsync(user);
            if (!(isValidCredentials && isConfirmedEmail))
            {
                return null;
            }
            var token = await tokenRepository.GenerateToken(user.Id);
            return token;
        }
        public async Task<bool> UserInRoleAsync(string id, string roleName)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return false;
            }
            var success = await userManager.IsInRoleAsync(user, roleName);
            if (!success)
            {
                return false;
            }
            return true;
        }
        public async Task<User> GetUserDetails(string? id)
        {
            var user = await userManager.Users
                .AsSplitQuery()
               .FirstOrDefaultAsync(u => u.Id.Equals(id));
            user.Devices = await userDbContext.Devices.Where(d => d.UserId == user.Id)
                .Select(d => new Device()
                {
                    Id = d.Id,
                    FcmToken = d.FcmToken,
                    IsActive = d.IsActive,
                    //LastLoggedInAt = d.LastLoggedInAt,
                    UserId = d.UserId,
                })
                .ToListAsync();
            return user;

        }
        public async Task<List<(User user, string? roleName)>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName)
        {
            var query = userDbContext.Users
                .AsQueryable();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email!.Contains(email));

            if (!string.IsNullOrEmpty(phoneNumber))
                query = query.Where(u => u.PhoneNumber!.Contains(phoneNumber));

            var result = await query
        .Join(userDbContext.UserRoles,
              u => u.Id,
              ur => ur.UserId,
              (u, ur) => new { u, ur })
        .Join(userDbContext.Roles,
              temp => temp.ur.RoleId,
              r => r.Id,
              (temp, r) => new
              {
                  user = temp.u,
                  roleName = r.Name
              })
        .Where(x => string.IsNullOrEmpty(role) || x.roleName.Contains(role))

        .ToListAsync();

            return result.Select(x => (x.user, x.roleName)).ToList();
        }

        public Task<List<User>> GetAssistants(string? sortByRating)
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetRoleOfUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "";
            }
            var role = await userManager.GetRolesAsync(user);
            return role.First();
        }

        public async Task<(User user, IdentityResult result)> UpdateUserAsync(User user)
        {
            var result = await userManager.UpdateAsync(user);

            return (user, result);
        }

        public async Task<IdentityResult> UpdatePassword(User user, string oldPassword, string newPassword)
        {
            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }

        //public async Task<IEnumerable<IdentityError>> ResetPassword(string token, string newPassword)
        //{
        //    var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.ForgotPasswordToken.Equals(token));

        //    var result = await userManager.ResetPasswordAsync(existingUser, token, newPassword);

        //    if (result.Succeeded)
        //    {
        //        existingUser.OTP = null;
        //        existingUser.ExpiryOtpDate = null;
        //        existingUser.ForgotPasswordToken = null;
        //        await userManager.UpdateAsync(existingUser);
        //    }
        //    return result.Errors;
        //    var errorList = new List<IdentityError>()
        //     {
        //         new()
        //         {
        //             Code = "User Doesnt't Exist",
        //             Description = "Couldn't Find User by Email",
        //         }
        //     };
        //    return errorList;

        //}

        public async Task<(bool IsValid, string Token)> VerifyForgotPasswordOtp(string code)
        {
            //var existingUser = await dbcontext.Users
            //    .FirstOrDefaultAsync(u => u.Otp == code);

            //if (existingUser == null)
            //    return (false, null);

            //var forgotPasswordToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
            //existingUser.ForgotPasswordToken = forgotPasswordToken;
            //await userManager.UpdateAsync(existingUser);

            //var isValid = existingUser.ExpiryOtpDate > DateTime.UtcNow;
            //await dbcontext.SaveChangesAsync();
            //return (isValid, forgotPasswordToken);
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> UpdateSecurityStampAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.UpdateSecurityStampAsync(user);
                return result;
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "User doesn't exist",
                Description = "User Doesn't exist"
            });
        }

        public async Task DeleteUser(User user)
        {
            user.IsDeleted = true;
            await userManager.UpdateAsync(user);
        }

        public async Task<User?> FindUserById(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
        public async Task<User?> FindUserByIdOptionalTracking(string userId, bool asNoTracking = false)
        {
            var query = userDbContext.Users.AsQueryable();
            if (asNoTracking == true)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<List<User>> GetAdmins()
        {
            var adminsWithDevices = await userDbContext.Users
                .Where(u => userDbContext.UserRoles
                    .Any(ur => ur.UserId == u.Id &&
                               userDbContext.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Administrator")))
                .Include(u => u.Devices)
                .ToListAsync();

            return adminsWithDevices;
        }

        public async Task<IdentityResult> AssistantRecoveryPassword(string userId, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "UserNotFound",
                    Description = $"User with ID '{userId}' was not found."
                });
            }

            var removeResult = await userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                return removeResult;
            }

            var addResult = await userManager.AddPasswordAsync(user, newPassword);
            return addResult;
        }

        public Task<IEnumerable<IdentityError>> ResetPassword(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedEntity<User>> GetAllPaginatedAsync(int page, int pageSize, string userName)
        {
            var query = userDbContext.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsQueryable();

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(u => !string.IsNullOrEmpty(u.UserName) && u.UserName.ToLower().Contains(userName.ToLower()));

            var users = await query.ToListAsync();

            var pagedEntity = new PagedEntity<User>();
            pagedEntity.Items = users;
            pagedEntity.TotalItems = userDbContext.Users.Count();
            pagedEntity.PageNumber = page;
            pagedEntity.PageSize = pageSize;

            return pagedEntity;
        }
        public async Task<List<User>> GetAllUsersNotPaginated(string userName)
        {
            var query = userDbContext.Users
           .AsQueryable();
            if (!string.IsNullOrEmpty(userName))
                query = query.Where(u => u.UserName.ToLower().Contains(userName.ToLower()));

            var users = await query.ToListAsync();

            return users;
        }
    }
}

