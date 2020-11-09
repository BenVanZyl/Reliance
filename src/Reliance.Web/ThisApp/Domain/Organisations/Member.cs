using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Domain.Organisation
{
    /// <summary>
    /// Belongs to an Originasation
    /// </summary>
    public class Member : DomainEntityWithIdWithAudit
    {
        #region Properties

        public long OrganisationId { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        [ForeignKey("OrganisationId")]
        public Organisation Organisation { get; private set; }

        #endregion //properties

        #region Methods

        public static async Task<Member> Create(IQueryExecutor executor, OrganisationMemberDto data)
        {
            //does data.Email exists?  no duplicates allowed
            var value = await executor.Execute(new Reliance.Web.ThisApp.Services.Queries.Organisations.GetOrganisationMemberQuery(data.OrgId, data.Email));
            if (value != null)
                throw new ThisAppException(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("Organisation Member"));

            value = new Member(data);
            await executor.Add<Member>(value);
            await executor.Save();

            return value;
        }

        protected Member() { }

        private Member(OrganisationMemberDto data)
        {
            OrganisationId = data.OrgId;  // can only be set at creation
            SetEmail(data.Email);
            SetName(data.Name);
            SetIsActive(data.IsActive);
        }

        public void SetEmail(string value)
        { //todo: add validatio
            if (Email != value)
                Email = value;
        }

        public void SetName(string value)
        {
            if (Name != value)
                Name = value;
        }

        public void SetIsActive(bool value)
        {
            if (IsActive != value)
                IsActive = value;
        }
        #endregion //methods

        #region Configuration

        internal class Mapping : IEntityTypeConfiguration<Member>
        {
            public void Configure(EntityTypeBuilder<Member> builder)
            {
                builder.ToTable("OrganisationMember", "Info");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");

                builder.Property(p => p.OrganisationId).IsRequired();
                builder.Property(p => p.Email).IsRequired();
                builder.Property(p => p.IsActive).IsRequired();

                builder.Property(p => p.Email).HasMaxLength(1024).IsRequired();
                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();

                builder.Property(p => p.IsActive).HasDefaultValue(true);

                // TODO: Setup relationship objects
            }
        }

        #endregion //config//config
    }
}
