import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SwalHelper } from 'src/app/shared/swal-helper';
import { Login } from 'src/app/domain/object-interface';
import { UtilityService } from 'src/app/shared/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData: Login = { userName: '', password: '' };

    constructor(
        private authService: AuthService,
        private router: Router,
        private utilityService: UtilityService
    ) { }

  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        this.utilityService.userName = response.userName;
        SwalHelper.showToast(response.message, 'success');
        this.authService.setLoginSuccess();
        this.router.navigate(['/main-layout']);
      },    
      error: (err) => {
        if (err.status === 401) {
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
