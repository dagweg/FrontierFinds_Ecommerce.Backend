namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using Microsoft.EntityFrameworkCore;

public class UserRepository(
  EfCoreContext context,
  IProductRepository productRepository,
  IMapper mapper
) : EfCoreRepository<User, UserId>(context), IUserRepository
{
  private readonly EfCoreContext context = context;
  private readonly IProductRepository productRepository = productRepository;

  public async Task<User?> GetByEmailAsync(Email email)
  {
    return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber)
  {
    return await context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
  }

  public async Task<CartResult?> GetCartAsync(UserId userId, PaginationParameters pagination)
  {
    var userWithCartItems = await context
      .Users.Include(u => u.Cart)
      .ThenInclude(c => c.Items)
      .FirstOrDefaultAsync(u => u.Id == userId);

    if (userWithCartItems is null || userWithCartItems.Cart is null)
      return null;

    var cartItems = userWithCartItems.Cart.Items;

    var productIds = cartItems.Select(ci => ci.ProductId);

    var productBulk = await productRepository.BulkGetByIdAsync(productIds);

    return new CartResult
    {
      TotalItems = productBulk.Count,
      TotalItemsFetched = cartItems.Count,
      TotalPrice = cartItems
        .Where(ci => productBulk.ContainsKey(ci.ProductId))
        .Sum(ci => productBulk[ci.ProductId].Price * ci.Quantity),
      Items = cartItems
        .AsQueryable()
        .Paginate(pagination)
        .Where(ci => productBulk.ContainsKey(ci.ProductId))
        .Select(ci => new CartItemResult
        {
          Id = ci.Id,
          Product = mapper.Map<ProductResult>(productBulk[ci.ProductId]),
          Quantity = ci.Quantity,
        }),
    };
  }

  // public async Task<Result> AddToCartRangeAsync(UserId userId, List<CartItem> cartItems)
  // {
  //   var user = await GetByIdAsync(userId);

  //   if (user is null)
  //     return NotFoundError.GetResult(nameof(userId), "User not found");

  //   var cartDict = user.Cart.Items.ToDictionary(kvp => kvp.ProductId, kvp => kvp);

  //   var productBulk = await productRepository.BulkGetByIdAsync(cartDict.Keys);

  //   var newCartItems = new List<CartItem>();

  //   foreach (var cartItem in cartItems)
  //   {
  //     if (
  //       productBulk.ContainsKey(cartItem.ProductId)
  //       && productBulk[cartItem.ProductId].SellerId == userId
  //     )
  //     {
  //       return BadRequestError.GetResult(
  //         nameof(cartItem.ProductId),
  //         "You cannot add your own product to cart"
  //       );
  //     }
  //     if (cartDict.ContainsKey(cartItem.ProductId) && productBulk.ContainsKey(cartItem.ProductId))
  //     {
  //       var availableStockQuantity = productBulk[cartItem.ProductId].Stock.Quantity;
  //       var requestedQuantity = cartDict[cartItem.ProductId].Quantity + cartItem.Quantity;

  //       if (requestedQuantity <= availableStockQuantity)
  //         cartDict[cartItem.ProductId].SetQuantity(requestedQuantity);
  //       else
  //         cartDict[cartItem.ProductId].SetQuantity(availableStockQuantity);

  //       newCartItems.Add(cartDict[cartItem.ProductId]);
  //     }
  //     else
  //     {
  //       newCartItems.Add(cartItem);
  //     }
  //   }

  //   // user.Cart.ClearCart();
  //   user.Cart.AddItemsRange(newCartItems);

  //   // update the user
  //   Update(user);

  //   return Result.Ok();
  // }

  public async Task<bool> RemoveFromCartRangeAsync(UserId userId, HashSet<CartItemId> cartItemIds)
  {
    var user = await GetByIdAsync(userId);

    if (user is null)
      return false;

    user.Cart.RemoveItems(cartItemIds);

    Update(user);

    return true;
  }

  public async Task<bool> UpdateCartAsync(UserId userId, Dictionary<CartItemId, int> cartItems)
  {
    var user = await GetByIdAsync(userId);
    if (user is null)
      return false;

    foreach (var cartItem in user.Cart.Items)
    {
      if (cartItems.ContainsKey(cartItem.Id))
      {
        cartItem.SetQuantity(cartItems[cartItem.Id]);
      }
    }

    Update(user);

    return true;
  }

  public async Task<bool> AddToWishlistRangeAsync(UserId userId, List<ProductId> productIds)
  {
    var user = await GetByIdAsync(userId);
    if (user is null)
      return false;

    user.Wishlist.AddProductsRange(
      productIds.Where(pi => user.Products.All(p => p.Id != pi)).ToList()
    );

    Update(user);

    return true;
  }

  public async Task<bool> RemoveFromWishlistRangeAsync(UserId userId, HashSet<ProductId> productIds)
  {
    var user = await GetByIdAsync(userId);
    if (user is null)
      return false;

    user.Wishlist.RemoveProductsRange(productIds);

    Update(user);

    return true;
  }

  public async Task<WishlistsResult?> GetWishlistsAsync(
    UserId userId,
    PaginationParameters pagination
  )
  {
    var user = await GetByIdAsync(userId);
    if (user is null)
      return null;

    var result = new WishlistsResult
    {
      TotalItems = user.Wishlist.ProductIds.Count,
      Wishlists = (
        await productRepository.BulkGetByIdAsync(user.Wishlist.ProductIds.AsEnumerable())
      ).Select(kvp => mapper.Map<ProductResult>(kvp.Value)),
    };

    return result;
  }

  public async Task<IDictionary<Guid, User>> BulkGetById(IEnumerable<Guid> userIds)
  {
    var userIdSet = userIds.ToArray();

    // Expression<Func<User, bool>> predicate = user => false; // Start with false
    // foreach (var userId in userIdSet)
    // {
    //   Expression<Func<User, bool>> singlePredicate = user => user.Id.Value == userId;
    //   predicate = Expression.Lambda<Func<User, bool>>(
    //     Expression.OrElse(predicate.Body, singlePredicate.Body),
    //     predicate.Parameters
    //   );
    // }

    return context
      .Users.AsEnumerable()
      .Where(x => userIds.Any(y => y == x.Id.Value))
      .ToDictionary(k => k.Id.Value, v => v);
  }
}
