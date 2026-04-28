import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../models/product.model';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css'],
    standalone: false
})
export class CartComponent implements OnInit {
    cartItems: CartItem[] = [];

    constructor(public cartService: CartService) { }

    ngOnInit(): void {
        this.cartService.cartItems$.subscribe(items => {
            this.cartItems = items;
        });
    }

    removeItem(id: number) {
        this.cartService.removeFromCart(id).subscribe();
    }

    getTotalPrice(): number {
        return this.cartItems.reduce((acc, item) => acc + (item.product?.price || 0) * item.quantity, 0);
    }
}
