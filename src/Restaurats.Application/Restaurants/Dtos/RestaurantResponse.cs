﻿namespace Restaurats.Application.Restaurants.Dtos;
public record RestaurantResponse(
    int Id,
    string Name,
    string Description,
    string Category,
    bool HasDelivery,
    string City,
    string Street,
    string PostalCode
    );
