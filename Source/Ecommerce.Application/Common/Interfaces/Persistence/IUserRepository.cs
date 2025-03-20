namespace Ecommerce.Application.Common.Interfaces.Persistence;

using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

public interface IUserRepository : IRepository<User, UserId>
{
  /// <summary>
  /// Gets a User that matches the given email
  /// </summary>
  /// <param name="email"></param>
  /// <returns>
  ///   A user entity or null (if not found)
  /// </returns>
  Task<User?> GetByEmailAsync(Email email);

  /// <summary>
  /// Gets a User that matches the given phoneNumber
  /// </summary>
  /// <param name="phoneNumber"></param>
  /// <returns>
  ///   A user entity or null (if not found)
  /// </returns>
  Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber);

  /// <summary>
  /// Gets list of cart items for the specified user
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="pagination"></param>
  /// <returns>
  ///  CartResult if user is found, other wise null
  /// </returns>
  Task<CartResult?> GetCartAsync(UserId userId, PaginationParameters pagination);

  /// <summary>
  /// Adds a list of cart items to the user's cart
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="cartItems"></param>
  /// <returns>
  ///   True if carts are added successfully, false if either user doesn't exist or problem in adding to cart
  /// </returns>
  // Task<Result> AddToCartRangeAsync(UserId userId, List<CartItem> cartItems);

  /// <summary>
  /// Removes a list of cart items from the user's cart
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="cartItemIds"></param>
  /// <returns></returns>
  Task<bool> RemoveFromCartRangeAsync(UserId userId, HashSet<CartItemId> cartItemIds);

  /// <summary>
  /// Updates the cart items for the specified user
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="cartItems"></param>
  /// <returns></returns>
  Task<bool> UpdateCartAsync(UserId userId, Dictionary<CartItemId, int> cartItems);

  /// <summary>
  /// Adds a list of products to the user's wishlist
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="productIds"></param>
  /// <returns></returns>
  Task<bool> AddToWishlistRangeAsync(UserId userId, List<ProductId> productIds);

  /// <summary>
  /// Removes a list of products from the user's wishlist
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="productIds"></param>
  /// <returns></returns>
  Task<bool> RemoveFromWishlistRangeAsync(UserId userId, HashSet<ProductId> productIds);

  /// <summary>
  /// Gets the wishlist items for the specified user
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="pagination"></param>
  /// <returns></returns>
  Task<WishlistsResult?> GetWishlistsAsync(UserId userId, PaginationParameters pagination);

  /// <summary>
  /// Gets a list of users by their ids
  /// </summary>
  /// <param name="userIds"></param>
  /// <returns></returns>
  Task<IDictionary<Guid, User>> BulkGetById(IEnumerable<Guid> userIds);
}
