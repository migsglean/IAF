import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PartService } from '../services/part.service';
import { PartsDetails, PartsDto } from '../domain/object-interface';
import { SwalHelper } from '../shared/swal-helper';
import { PartDialogComponent } from '../part-dialog/part-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-parts',
  templateUrl: './parts.component.html',
  styleUrls: ['./parts.component.css']
})
export class PartsComponent {
    displayedColumns: string[] = ['partsId', 'partsDesc', 'quantity', 'image', 'actions'];
    dataSource = new MatTableDataSource<PartsDetails>();

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(
        private partsService: PartService,
        private dialog: MatDialog) { }

    ngOnInit() {
        this.loadParts();
    }

    loadParts() {
        this.partsService.getParts().subscribe({
            next: (response: PartsDto) => {
                this.dataSource.data = response.partsDetails;
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            },
            error: (err) => {
                if (err.error && err.error.responseDefaultDto) {
                    SwalHelper.error(err.error.responseDefaultDto.message);
                } else {
                    SwalHelper.error('Unexpected error occurred while fetching products.');
                }
            }
        });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    addPart() {
        const dialogRef = this.dialog.open(PartDialogComponent, {
            width: '500px',
            data: { mode: 'add' }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.partsService.addPart(result).subscribe({
                    next: (response) => {
                        SwalHelper.success(response.message);
                        this.loadParts();
                    },
                    error: (err) => {
                        const message = err.error.message
                            || 'Unexpected error occurred while adding part.';
                        SwalHelper.error(message);
                    }
                });
            }
        });
    }

    editPart(part: PartsDetails) {
        const dialogRef = this.dialog.open(PartDialogComponent, {
            width: '500px',
            data: { mode: 'edit', part }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.partsService.updatePart(result).subscribe({
                    next: (response) => {
                        SwalHelper.success(response.message);
                        this.loadParts();
                    },
                    error: (err) => {
                        console.log(err)
                        const message = err.error.message
                            || 'Unexpected error occurred while updating part.';
                        SwalHelper.error(message);
                    }
                });
            }
        });
    }

    deletePart(partsId: string) {
        SwalHelper.confirm('Are you sure you want to delete this part?', () => {
            this.partsService.deletePart(partsId).subscribe({
                next: (response) => {
                    SwalHelper.success(response.message);
                    this.loadParts();
                },
                error: (err) => {
                    const message = err.error.message
                        || 'Unexpected error occurred while deleting part.';
                    SwalHelper.error(message);
                }
            });
        });
    }
}
