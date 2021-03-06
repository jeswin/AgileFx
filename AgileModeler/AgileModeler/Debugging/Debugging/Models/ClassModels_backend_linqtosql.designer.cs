﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Debugging.Models.L2S
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="canyoucodeDb")]
	public partial class canyoucodeDb_LinqToSqlDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAccount(Account instance);
    partial void UpdateAccount(Account instance);
    partial void DeleteAccount(Account instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertCompany(Company instance);
    partial void UpdateCompany(Company instance);
    partial void DeleteCompany(Company instance);
    partial void InsertProject(Project instance);
    partial void UpdateProject(Project instance);
    partial void DeleteProject(Project instance);
    partial void InsertPortfolio(Portfolio instance);
    partial void UpdatePortfolio(Portfolio instance);
    partial void DeletePortfolio(Portfolio instance);
    partial void InsertPortfolioEntry(PortfolioEntry instance);
    partial void UpdatePortfolioEntry(PortfolioEntry instance);
    partial void DeletePortfolioEntry(PortfolioEntry instance);
    partial void InsertEntity1(Entity1 instance);
    partial void UpdateEntity1(Entity1 instance);
    partial void DeleteEntity1(Entity1 instance);
    partial void InsertEntity2(Entity2 instance);
    partial void UpdateEntity2(Entity2 instance);
    partial void DeleteEntity2(Entity2 instance);
    partial void InsertEntity3(Entity3 instance);
    partial void UpdateEntity3(Entity3 instance);
    partial void DeleteEntity3(Entity3 instance);
    #endregion
		
		public canyoucodeDb_LinqToSqlDataContext() : 
				base("Data Source=.;Initial Catalog=canyoucodeDb;Integrated Security=True", mappingSource)
		{
			OnCreated();
		}
		
		public canyoucodeDb_LinqToSqlDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public canyoucodeDb_LinqToSqlDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public canyoucodeDb_LinqToSqlDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public canyoucodeDb_LinqToSqlDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Account> Account
		{
			get
			{
				return this.GetTable<Account>();
			}
		}
		
		public System.Data.Linq.Table<User> User
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<Company> Company
		{
			get
			{
				return this.GetTable<Company>();
			}
		}
		
		public System.Data.Linq.Table<Project> Project
		{
			get
			{
				return this.GetTable<Project>();
			}
		}
		
		public System.Data.Linq.Table<Portfolio> Portfolio
		{
			get
			{
				return this.GetTable<Portfolio>();
			}
		}
		
		public System.Data.Linq.Table<PortfolioEntry> PortfolioEntry
		{
			get
			{
				return this.GetTable<PortfolioEntry>();
			}
		}
		
		public System.Data.Linq.Table<Entity1> Entity1
		{
			get
			{
				return this.GetTable<Entity1>();
			}
		}
		
		public System.Data.Linq.Table<Entity2> Entity2
		{
			get
			{
				return this.GetTable<Entity2>();
			}
		}
		
		public System.Data.Linq.Table<Entity3> Entity3
		{
			get
			{
				return this.GetTable<Entity3>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Account")]
	public partial class Account : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Account()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_User", Storage="_User", ThisKey="Id", OtherKey="AccountId", IsUnique=true, IsForeignKey=false)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.Account = null;
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.Account = this;
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.User")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private long _AccountId;
		
		private long _CompanyId;
		
		private EntityRef<Account> _Account;
		
		private EntityRef<Company> _Company;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnAccountIdChanging(long value);
    partial void OnAccountIdChanged();
    partial void OnCompanyIdChanging(long value);
    partial void OnCompanyIdChanged();
    #endregion
		
		public User()
		{
			this._Account = default(EntityRef<Account>);
			this._Company = default(EntityRef<Company>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Account", Storage="_AccountId", DbType="BigInt NOT NULL")]
		public long AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				if ((this._AccountId != value))
				{
					this.OnAccountIdChanging(value);
					this.SendPropertyChanging();
					this._AccountId = value;
					this.SendPropertyChanged("AccountId");
					this.OnAccountIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Company", Storage="_CompanyId", DbType="BigInt NOT NULL")]
		public long CompanyId
		{
			get
			{
				return this._CompanyId;
			}
			set
			{
				if ((this._CompanyId != value))
				{
					this.OnCompanyIdChanging(value);
					this.SendPropertyChanging();
					this._CompanyId = value;
					this.SendPropertyChanged("CompanyId");
					this.OnCompanyIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_User", Storage="_Account", ThisKey="AccountId", OtherKey="Id", IsForeignKey=true)]
		public Account Account
		{
			get
			{
				return this._Account.Entity;
			}
			set
			{
				Account previousValue = this._Account.Entity;
				if (((previousValue != value) 
							|| (this._Account.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Account.Entity = null;
						previousValue.User = null;
					}
					this._Account.Entity = value;
					if ((value != null))
					{
						value.User = this;
						this._AccountId = value.Id;
					}
					else
					{
						this._AccountId = default(long);
					}
					this.SendPropertyChanged("Account");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Company_User", Storage="_Company", ThisKey="CompanyId", OtherKey="Id", IsForeignKey=true)]
		public Company Company
		{
			get
			{
				return this._Company.Entity;
			}
			set
			{
				Company previousValue = this._Company.Entity;
				if (((previousValue != value) 
							|| (this._Company.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Company.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._Company.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._CompanyId = value.Id;
					}
					else
					{
						this._CompanyId = default(long);
					}
					this.SendPropertyChanged("Company");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Company")]
	public partial class Company : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private string _Name;
		
		private string _Type;
		
		private long _PortfolioId;
		
		private EntitySet<User> _Users;
		
		private EntityRef<Portfolio> _Portfolio;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnPortfolioIdChanging(long value);
    partial void OnPortfolioIdChanged();
    #endregion
		
		public Company()
		{
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			this._Portfolio = default(EntityRef<Portfolio>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Portfolio", Storage="_PortfolioId", DbType="BigInt NOT NULL")]
		public long PortfolioId
		{
			get
			{
				return this._PortfolioId;
			}
			set
			{
				if ((this._PortfolioId != value))
				{
					this.OnPortfolioIdChanging(value);
					this.SendPropertyChanging();
					this._PortfolioId = value;
					this.SendPropertyChanged("PortfolioId");
					this.OnPortfolioIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Company_User", Storage="_Users", ThisKey="Id", OtherKey="CompanyId")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Portfolio_Company", Storage="_Portfolio", ThisKey="PortfolioId", OtherKey="Id", IsForeignKey=true)]
		public Portfolio Portfolio
		{
			get
			{
				return this._Portfolio.Entity;
			}
			set
			{
				Portfolio previousValue = this._Portfolio.Entity;
				if (((previousValue != value) 
							|| (this._Portfolio.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Portfolio.Entity = null;
						previousValue.Company = null;
					}
					this._Portfolio.Entity = value;
					if ((value != null))
					{
						value.Company = this;
						this._PortfolioId = value.Id;
					}
					else
					{
						this._PortfolioId = default(long);
					}
					this.SendPropertyChanged("Portfolio");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Company = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Company = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Project")]
	public partial class Project : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Project()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Portfolio")]
	public partial class Portfolio : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private EntityRef<Company> _Company;
		
		private EntitySet<PortfolioEntry> _PortfolioEntries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Portfolio()
		{
			this._Company = default(EntityRef<Company>);
			this._PortfolioEntries = new EntitySet<PortfolioEntry>(new Action<PortfolioEntry>(this.attach_PortfolioEntries), new Action<PortfolioEntry>(this.detach_PortfolioEntries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Portfolio_Company", Storage="_Company", ThisKey="Id", OtherKey="PortfolioId", IsUnique=true, IsForeignKey=false)]
		public Company Company
		{
			get
			{
				return this._Company.Entity;
			}
			set
			{
				Company previousValue = this._Company.Entity;
				if (((previousValue != value) 
							|| (this._Company.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Company.Entity = null;
						previousValue.Portfolio = null;
					}
					this._Company.Entity = value;
					if ((value != null))
					{
						value.Portfolio = this;
					}
					this.SendPropertyChanged("Company");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Portfolio_PortfolioEntry", Storage="_PortfolioEntries", ThisKey="Id", OtherKey="PortfolioId")]
		public EntitySet<PortfolioEntry> PortfolioEntries
		{
			get
			{
				return this._PortfolioEntries;
			}
			set
			{
				this._PortfolioEntries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PortfolioEntries(PortfolioEntry entity)
		{
			this.SendPropertyChanging();
			entity.Portfolio = this;
		}
		
		private void detach_PortfolioEntries(PortfolioEntry entity)
		{
			this.SendPropertyChanging();
			entity.Portfolio = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PortfolioEntry")]
	public partial class PortfolioEntry : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private long _PortfolioId;
		
		private EntityRef<Portfolio> _Portfolio;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnPortfolioIdChanging(long value);
    partial void OnPortfolioIdChanged();
    #endregion
		
		public PortfolioEntry()
		{
			this._Portfolio = default(EntityRef<Portfolio>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Portfolio", Storage="_PortfolioId", DbType="BigInt NOT NULL")]
		public long PortfolioId
		{
			get
			{
				return this._PortfolioId;
			}
			set
			{
				if ((this._PortfolioId != value))
				{
					this.OnPortfolioIdChanging(value);
					this.SendPropertyChanging();
					this._PortfolioId = value;
					this.SendPropertyChanged("PortfolioId");
					this.OnPortfolioIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Portfolio_PortfolioEntry", Storage="_Portfolio", ThisKey="PortfolioId", OtherKey="Id", IsForeignKey=true)]
		public Portfolio Portfolio
		{
			get
			{
				return this._Portfolio.Entity;
			}
			set
			{
				Portfolio previousValue = this._Portfolio.Entity;
				if (((previousValue != value) 
							|| (this._Portfolio.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Portfolio.Entity = null;
						previousValue.PortfolioEntries.Remove(this);
					}
					this._Portfolio.Entity = value;
					if ((value != null))
					{
						value.PortfolioEntries.Add(this);
						this._PortfolioId = value.Id;
					}
					else
					{
						this._PortfolioId = default(long);
					}
					this.SendPropertyChanged("Portfolio");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Entity1")]
	public partial class Entity1 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Entity1()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Entity2")]
	public partial class Entity2 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private EntityRef<Entity1> _Entity1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Entity2()
		{
			this._Entity1 = default(EntityRef<Entity1>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					if (this._Entity1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Entity1_Entity2", Storage="_Entity1", ThisKey="Id", OtherKey="Id", IsForeignKey=true)]
		public Entity1 Entity1
		{
			get
			{
				return this._Entity1.Entity;
			}
			set
			{
				if ((this._Entity1.Entity != value))
				{
					this.SendPropertyChanging();
					this._Entity1.Entity = value;
					this.SendPropertyChanged("Entity1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Entity3")]
	public partial class Entity3 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id1;
		
		private long _Id;
		
		private EntityRef<Entity1> _Entity1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnId1Changing(long value);
    partial void OnId1Changed();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    #endregion
		
		public Entity3()
		{
			this._Entity1 = default(EntityRef<Entity1>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id1", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id1
		{
			get
			{
				return this._Id1;
			}
			set
			{
				if ((this._Id1 != value))
				{
					this.OnId1Changing(value);
					this.SendPropertyChanging();
					this._Id1 = value;
					this.SendPropertyChanged("Id1");
					this.OnId1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					if (this._Entity1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Entity1_Entity3", Storage="_Entity1", ThisKey="Id", OtherKey="Id", IsForeignKey=true)]
		public Entity1 Entity1
		{
			get
			{
				return this._Entity1.Entity;
			}
			set
			{
				if ((this._Entity1.Entity != value))
				{
					this.SendPropertyChanging();
					this._Entity1.Entity = value;
					this.SendPropertyChanged("Entity1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
