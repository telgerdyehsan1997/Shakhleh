using Domain;
using Olive.Mvc;

public abstract class BaseView<TModel> : RazorPage<TModel>
{
    /// <summary>Gets a Domain User object extracted from the current user principal.</summary>
    public User GetUser() => User.Extract<User>();

    public CompanyUser CompanyUser => GetUser() as CompanyUser;
    public ChannelPortsUser ChannelPortsUser => GetUser() as ChannelPortsUser;
}