export interface CheckoutItem {
  sku: string;
  name: string;
  unitPrice: number;
  pricingRule?: BulkPricing;
}

export interface BulkPricing {
  bulkQuantity: number;
  bulkPrice: number;
}

export interface CartItem {
  sku: string;
  quantity: number;
}

export interface CartResponse {
  items: CartItem[];
  total: number;
}
