import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SwalHelper } from 'src/app/shared/swal-helper';
import { SignUp } from 'src/app/domain/object-interface';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignUpComponent {
    signUpData: SignUp = { userName: '', emailAddress: '', password: '', confirmPassword: '' };

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    onSignUp() {
        console.log('Sign-up data:', this.signUpData);
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!emailPattern.test(this.signUpData.emailAddress)) {
            SwalHelper.warning('Invalid email format');
            return;
        }

        if (this.signUpData.password !== this.signUpData.confirmPassword) {
            SwalHelper.error('Passwords do not match!');
            return;
        }

        this.authService.signUp(this.signUpData).subscribe({
            next: (response) => {
                SwalHelper.success(response.message);

                this.router.navigate(['/auth/login']);
            },
            error: (err) => {
                if (err.status === 409) {
                    SwalHelper.error(err.error.message);
                } else if (err.status === 400) {
                    SwalHelper.error(err.error.message);
                } else if (err.status === 500) {
                    SwalHelper.error(err.error.message);
                } else {
                    SwalHelper.error('Unexpected error occurred.');
                }
            }
        });
    }
}
