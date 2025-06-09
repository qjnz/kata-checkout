import { useState, useEffect } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { ShoppingCart, Plus, Trash2 } from 'lucide-react';
import type { CheckoutItem, CartItem, CartResponse } from '@/types'

const API_BASE_URL = 'http://localhost:5132/api';

export default function Checkout() {
  const [items, setItems] = useState<CheckoutItem[]>([]);
  const [cart, setCart] = useState<CartResponse>({ items: [], total: 0 });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchItems();
    fetchCart();
  }, []);

  const fetchItems = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/checkout/items`);
      if (!response.ok) throw new Error('Failed to fetch items');
      const data = await response.json();
      setItems(data);
    } catch (err) {
      setError('Failed to load items');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const fetchCart = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/checkout/cart`);
      if (!response.ok) throw new Error('Failed to fetch cart');
      const data = await response.json();
      setCart(data);
    } catch (err) {
      console.error('Failed to fetch cart:', err);
    }
  };

  const addToCart = async (sku: string) => {
    try {
      const response = await fetch(`${API_BASE_URL}/checkout/scan/${sku}`, {
        method: 'POST',
      });
      if (!response.ok) throw new Error('Failed to add item');
      await fetchCart();
    } catch (err) {
      setError(`Failed to add item: ${err}`);
    }
  };

  const clearCart = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/checkout/clear`, {
        method: 'POST',
      });
      if (!response.ok) throw new Error('Failed to clear cart');
      await fetchCart();
    } catch (err) {
      setError(`Failed to clear cart: ${err}`);
    }
  };

  const getCartItemQuantity = (sku: string) => {
    const cartItem = cart.items.find(item => item.sku === sku);
    return cartItem ? cartItem.quantity : 0;
  };

  const formatPrice = (price: number) => {
    return `$${price.toFixed(2)}`;
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500 mx-auto mb-4"></div>
          <p className="text-gray-600">Loading checkout...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 p-4">
      <div className="max-w-6xl mx-auto">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-4xl font-bold text-gray-900 mb-2">
            <ShoppingCart className="inline-block mr-3 h-10 w-10" />
            Checkout System
          </h1>
          <p className="text-gray-600">Add items to your cart and see the total with special pricing</p>
        </div>

        {error && (
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-6">
            {error}
            <button
              onClick={() => setError(null)}
              className="float-right font-bold text-red-700 hover:text-red-900"
            >
              ×
            </button>
          </div>
        )}

        <div className="grid lg:grid-cols-3 gap-8">
          {/* Items List */}
          <div className="lg:col-span-2">
            <Card>
              <CardHeader>
                <CardTitle>Available Items</CardTitle>
                <CardDescription>
                  Click "Add to Cart" to add items. Special pricing applies automatically!
                </CardDescription>
              </CardHeader>
              <CardContent>
                <div className="grid md:grid-cols-2 gap-4">
                  {items.map((item) => (
                    <div key={item.sku} className="border rounded-lg p-4 hover:shadow-md transition-shadow">
                      <div className="flex justify-between items-start mb-3">
                        <div>
                          <h3 className="text-lg font-semibold">{item.name}</h3>
                          <p className="text-sm text-gray-600">SKU: {item.sku}</p>
                        </div>
                        <Badge variant="secondary" className="text-lg font-bold">
                          {formatPrice(item.unitPrice)}
                        </Badge>
                      </div>

                      {item.pricingRule?.bulkQuantity && item.pricingRule?.bulkPrice && (
                        <div className="bg-green-50 border border-green-200 rounded p-2 mb-3">
                          <p className="text-sm text-green-800 font-medium">
                            Special Offer: {item.pricingRule.bulkQuantity} for {formatPrice(item.pricingRule.bulkPrice)}
                          </p>
                        </div>
                      )}

                      <div className="flex items-center justify-between">
                        <Button
                          onClick={() => addToCart(item.sku)}
                          className="flex items-center gap-2"
                          size="sm"
                        >
                          <Plus className="h-4 w-4" />
                          Add to Cart
                        </Button>

                        {getCartItemQuantity(item.sku) > 0 && (
                          <Badge variant="outline" className="bg-blue-50">
                            {getCartItemQuantity(item.sku)} in cart
                          </Badge>
                        )}
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Cart Summary */}
          <div className="lg:col-span-1">
            <Card className="sticky top-4">
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <ShoppingCart className="h-5 w-5" />
                  Shopping Cart
                </CardTitle>
              </CardHeader>
              <CardContent>
                {cart.items.length === 0 ? (
                  <div className="text-center py-8 text-gray-500">
                    <ShoppingCart className="h-12 w-12 mx-auto mb-3 opacity-50" />
                    <p>Your cart is empty</p>
                  </div>
                ) : (
                  <div className="space-y-4">
                    {cart.items.map((cartItem) => {
                      const item = items.find(i => i.sku === cartItem.sku);
                      if (!item) return null;

                      return (
                        <div key={cartItem.sku} className="flex justify-between items-center py-2 border-b">
                          <div>
                            <p className="font-medium">{item.name}</p>
                            <p className="text-sm text-gray-600">
                              {cartItem.quantity} × {formatPrice(item.unitPrice)}
                            </p>
                          </div>
                          <Badge variant="outline">
                            {cartItem.quantity}
                          </Badge>
                        </div>
                      );
                    })}

                    <div className="pt-4 border-t">
                      <div className="flex justify-between items-center text-xl font-bold">
                        <span>Total:</span>
                        <span className="text-green-600">{formatPrice(cart.total)}</span>
                      </div>
                    </div>

                    <Button
                      onClick={clearCart}
                      variant="outline"
                      className="w-full mt-4 text-red-600 border-red-200 hover:bg-red-50"
                    >
                      <Trash2 className="h-4 w-4 mr-2" />
                      Clear Cart
                    </Button>
                  </div>
                )}
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </div>
  );
}
