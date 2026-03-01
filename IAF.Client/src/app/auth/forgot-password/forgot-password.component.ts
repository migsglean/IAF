import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { SwalHelper } from 'src/app/shared/swal-helper';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  forgotData = { username: '' };

  constructor(private authService: AuthService) { }

  onForgotPassword() {
    this.authService.forgotPassword(this.forgotData).subscribe({
      next: () => {
        SwalHelper.success('Password reset instructions sent to your email.');
      },
      error: () => {
        SwalHelper.error('Error resetting password.');
      }
    });
  }
}
