using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurats.Domain.Entities;
using Restaurats.Infrastructure.Authorization.Constants;

namespace Restaurats.Infrastructure.Authorization;
internal class RestaurantUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, 
    RoleManager<IdentityRole> roleManager, 
    IOptions<IdentityOptions> options) 
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, options)
{
    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await  GenerateClaimsAsync(user);

        if(!string.IsNullOrWhiteSpace(user.Nationality))
            principal.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

        if(user.DateOfBirth.HasValue)
            principal.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(principal);

    }
}
//💡 إزاي العملية كلها بتشتغل خطوة بخطوة؟
//المستخدم يسجل دخول → السيستم يستدعي CreateAsync(user).
//ASP.NET يجيب الـ Claims الافتراضية للمستخدم باستخدام GenerateClaimsAsync(user).
//إحنا بنضيف عليه بيانات إضافية زي "nationality" و "dateOfBirth".
//الـ ClaimsPrincipal النهائي بيتخزن في جلسة تسجيل الدخول(Authentication Cookie أو Token).
//أي Request بعد كده يقدر يقرأ الـ Claims ويعرف المستخدم ده مين وإيه صلاحياته.