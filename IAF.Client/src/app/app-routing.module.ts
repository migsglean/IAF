import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './auth/auth.guard';

// Import Components for routing
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/signup/signup.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { ProductsComponent } from './products/products.component';
import { PartsComponent } from './parts/parts.component';

const routes: Routes = [

    { path: 'auth/login', component: LoginComponent },
    { path: 'auth/signup', component: SignUpComponent },
    { path: 'auth/forgot-password', component: ForgotPasswordComponent },

    {
        path: 'main-layout',
        component: MainLayoutComponent,
        canActivate: [authGuard],
        children: [
            { path: 'products', component: ProductsComponent },
            { path: 'parts', component: PartsComponent },
            { path: '', redirectTo: 'products', pathMatch: 'full' }
        ]
    },

    // Redirect empty path to login
    { path: '', redirectTo: 'auth/login', pathMatch: 'full' },

    // Wildcard route for 404
    { path: '**', redirectTo: 'auth/login' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
