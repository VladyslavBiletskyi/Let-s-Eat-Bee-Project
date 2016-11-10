using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Let_s_Eat_Bee_Project.Models
{
    public class AbstractUser
    {
        public AbstractUser()
        {
            this.Orders = new HashSet<Order>();
            this.Joinings = new HashSet<Joining>();
            this.Messages = new HashSet<Message>();
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Joining> Joinings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
    }
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
    public class UnauthorizedUser : AbstractUser
    { }
    public class AuthorizedUser : AbstractUser
    {
        public string Organization { get; set; }
        public string AppUserId { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }

    public class Order
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Joinings = new HashSet<Joining>();
            this.Messages = new HashSet<Message>();

        }

        public int Id { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public string Address { get; set; }
        public int CreatorId { get; set; }

        public virtual AbstractUser Creator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Joining> Joinings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
    }
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public virtual Order Order { get; set; }
        public virtual AbstractUser User { get; set; }
    }
    public class Joining
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }

        public virtual AbstractUser User { get; set; }
        public virtual Order Order { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AuthorizedUser> AuthUser { get; set; }
        public DbSet<AbstractUser> AllUsers { get; set; }
        public DbSet<Joining> Joinings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext()
            : base("LEBDatabase", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Order>()
                .HasOptional(a => a.Creator)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);
        }
    }
    public class DBIntializator : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            UnauthorizedUser u = new UnauthorizedUser();
            u.FirstName = "kek1";
            u.LastName = "kek2";
            context.AllUsers.Add(u);
            context.SaveChanges();
        }
    }
}