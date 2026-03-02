import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UtilityService } from 'src/app/shared/utility.service';
import { SwalHelper } from '../swal-helper';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {
    userName: string = '';
    showUserMenu = false;

    constructor(
        private utilityService: UtilityService,
        private router: Router
    ) { }

    ngOnInit() {
        this.userName = this.utilityService.userName;
    }

    toggleUserMenu() {
        this.showUserMenu = !this.showUserMenu;
    }

    goToSettings() {
        SwalHelper.warning('This feature not yet implemented.')
    }

    logout() {
        this.utilityService.userName = '';
        this.utilityService.clearProducts();
        this.utilityService.clearParts();

        this.router.navigate(['/login']);

        SwalHelper.showToast('You have been logged out successfully.', 'success');
    }
}
