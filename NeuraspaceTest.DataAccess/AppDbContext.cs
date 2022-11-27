// -----------------------------------------------------------------------
//  <copyright file="AppDbContext.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using NeuraspaceTest.Models;

namespace NeuraspaceTest.DataAccess
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CollisionEvent> CollisionEvents { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Satellite> Satellites { get; set; }

        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may
        ///         not have been passed
        ///         to the constructor, you can use
        ///         <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see
        ///             cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
        ///         .
        ///     </para>
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        ///     for more information.
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; Database=Exceryasla; Username=root; Password=password");
            }

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}