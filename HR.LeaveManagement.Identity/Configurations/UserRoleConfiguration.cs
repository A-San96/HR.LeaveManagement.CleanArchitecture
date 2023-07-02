using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        string adminRoleId = "e4a52831-c19e-469e-a3d7-f30ebe5badec";
        string employeeRoleId = "9e560806-f61f-4031-ae9b-a9faad6824f9";

        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = "526033fb-d870-4f16-bdc9-74dbed29a281"
            },
            new IdentityUserRole<string>
            {
                RoleId = employeeRoleId,
                UserId = "e67a49ce-0022-477e-9b16-69c705a0d99a"
            }
        );
    }
}
