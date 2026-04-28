export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  stock: number;
  categoryId: number;
  category?: Category;
}

export interface Category {
  id: number;
  name: string;
}

export interface CartItem {
  id: number;
  productId: number;
  product?: Product;
  quantity: number;
}

export interface User {
  id: number;
  username: string;
  email: string;
  role: string;
}
