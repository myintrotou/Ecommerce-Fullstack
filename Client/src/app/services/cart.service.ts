import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CartItem } from '../models/product.model';
import { AuthService } from './auth.service';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    private apiUrl = `${environment.apiUrl}/cart`;
    private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);
    cartItems$ = this.cartItemsSubject.asObservable();

    constructor(private http: HttpClient, private authService: AuthService) {
        if (this.authService.isLoggedIn()) {
            this.loadCart();
        }
    }

    loadCart() {
        this.http.get<CartItem[]>(this.apiUrl).subscribe({
            next: (items) => this.cartItemsSubject.next(items),
            error: () => this.cartItemsSubject.next([])
        });
    }

    addToCart(productId: number, quantity: number = 1): Observable<any> {
        return this.http.post(`${this.apiUrl}/add?productId=${productId}&quantity=${quantity}`, {}).pipe(
            tap(() => this.loadCart())
        );
    }

    removeFromCart(id: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`).pipe(
            tap(() => this.loadCart())
        );
    }

    getCartCount(): number {
        return this.cartItemsSubject.value.reduce((acc, item) => acc + item.quantity, 0);
    }
}
