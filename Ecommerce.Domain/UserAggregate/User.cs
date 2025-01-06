namespace Ecommerce.Domain.UserAggregate;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.NotificationAggregate;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.Events;
using Ecommerce.Domain.UserAggregate.Exceptions;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public class User : AggregateRoot<UserId>
{
  public Name FirstName { get; set; }
  public Name LastName { get; set; }
  public Email Email { get; set; }
  public Password Password { get; set; }
  public PhoneNumber PhoneNumber { get; set; }
  public string CountryCode { get; set; }
  public UserAddress Address { get; set; }
  public Wishlist Wishlist { get; set; }

  private List<Cart> _cart = [];
  private List<Order> _orders = [];
  private List<Product> _products = [];
  private List<Notification> _notifications = [];

  public IReadOnlyCollection<Cart> Cart => _cart.AsReadOnly();
  public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
  public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
  public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

  private User(
    UserId id,
    Name firstName,
    Name lastName,
    Email email,
    Password password,
    PhoneNumber phoneNumber,
    string countryCode,
    UserAddress address,
    Wishlist wishlist,
    List<Cart> cart,
    List<Order> orders,
    List<Product> products,
    List<Notification> notifications
  )
    : base(id)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
    PhoneNumber = phoneNumber;
    CountryCode = countryCode;
    Address = address;
    Wishlist = wishlist;
    _cart = cart;
    _orders = orders;
    _products = products;
    _notifications = notifications;
  }

  public static User Create(
    Name firstName,
    Name lastName,
    Email email,
    Password password,
    PhoneNumber phoneNumber,
    string countryCode,
    UserAddress? address = null,
    Wishlist? wishlist = null,
    List<Cart>? cart = null,
    List<Order>? orders = null,
    List<Product>? products = null,
    List<Notification>? notifications = null
  )
  {
    var user = new User(
      UserId.CreateUnique(),
      firstName,
      lastName,
      email,
      password,
      phoneNumber,
      countryCode,
      address ?? UserAddress.Create("", "", "", "", ""),
      wishlist ?? Wishlist.Create(),
      cart ?? [],
      orders ?? [],
      products ?? [],
      notifications ?? []
    );

    user.AddDomainEvent(new UserCreatedDomainEvent(user));

    return user;
  }

  public void ChangePassword(Password newPassword, Password currentPassword)
  {
    if (!Password.Equals(currentPassword))
      throw new InvalidCurrentPasswordException();

    Password = newPassword;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private User()
    : base(UserId.CreateUnique()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
