export interface CheckoutItem {
  sku: string;
  name: string;
  unitPrice: number;
  pricingRule?: {
    quantity: number;
    specialPrice: number;
  };
}

export interface CartItem {
  sku: string;
  quantity: number;
}

export interface CartResponse {
  items: CartItem[];
  total: number;
}
