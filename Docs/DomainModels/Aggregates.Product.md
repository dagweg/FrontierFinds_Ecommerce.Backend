# Product Aggregate

```json
{
  "id": { "value": "0000000-0000-0000-0000-000000000000" },
  "name": "Product Name",
  "description": "Product Description",
  "price": {
    "value": 0.0,
    "currency": "USD"
  },
  "stock": {
    "quantity": 0,
    "reserved": 0
  },
  "sellerId": "0000000-0000-0000-0000-000000000000",
  "categories": [{ "name": "Category Name" }, { "name": "Category Name" }],
  "tags": [{ "name": "Tag Name" }, { "name": "Tag Name" }],
  "thumbnail": {
    "id": { "value": "0000000-0000-0000-0000-000000000000" },
    "url": "https://example.com/image.jpg"
  },
  "images": {
    "id": { "value": "0000000-0000-0000-0000-000000000000" },
    "productId": { "value": "0000000-0000-0000-0000-000000000000" },
    "left": {
      "id": { "value": "0000000-0000-0000-0000-000000000000" },
      "url": "https://example.com/image.jpg",
      "fileName": "image.jpg",
      "fileSize": 0,
      "fileType": "image/jpeg"
    },
    "right": {
      "id": { "value": "0000000-0000-0000-0000-000000000000" },
      "url": "https://example.com/image.jpg",
      "fileName": "image.jpg",
      "fileSize": 0,
      "fileType": "image/jpeg"
    },
    "front": {
      "id": { "value": "0000000-0000-0000-0000-000000000000" },
      "url": "https://example.com/image.jpg",
      "fileName": "image.jpg",
      "fileSize": 0,
      "fileType": "image/jpeg"
    },
    "back": {
      "id": { "value": "0000000-0000-0000-0000-000000000000" },
      "url": "https://example.com/image.jpg",
      "fileName": "image.jpg",
      "fileSize": 0,
      "fileType": "image/jpeg"
    }
  },
  "rating": 4.5,
  "reviews": [
    {
      "id": { "value": "0000000-0000-0000-0000-000000000000" },
      "author": { "value": "0000000-0000-0000-0000-000000000000" },
      "description": "Review Content",
      "rating": 4.5,
      "createdAt": "2021-01-01T00:00:00.000Z",
      "updatedAt": "2021-01-01T00:00:00.000Z"
    }
  ],
  "promotion": {
    "discountPercentage": 10,
    "startDate": "2021-01-01T00:00:00.000Z",
    "endDate": "2021-01-01T00:00:00.000Z"
  },
  "createdAt": "2021-01-01T00:00:00.000Z",
  "updatedAt": "2021-01-01T00:00:00.000Z"
}
```
