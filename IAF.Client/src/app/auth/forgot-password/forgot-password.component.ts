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
        SwalHelper.warning('This feature not yet implemented.')
        //this.authService.forgotPassword(this.forgotData).subscribe({
        //  next: () => {
        //    SwalHelper.info('Password reset instructions sent to your email.');
        //  },
        //  error: () => {
        //    SwalHelper.error('Error resetting password.');
        //  }
        //});
    }
}
