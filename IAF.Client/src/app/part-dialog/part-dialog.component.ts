import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PartsDetails } from '../domain/object-interface';
import { UtilityService } from '../shared/utility.service';
import { CarParts, CellPhoneParts, DroneParts } from '../shared/product-parts.enum';

@Component({
    selector: 'app-part-dialog',
    templateUrl: './part-dialog.component.html',
    styleUrls: ['./part-dialog.component.css']
})
export class PartDialogComponent {
    partForm: FormGroup;
    mode: 'add' | 'edit';
    productSummaries: { productId: string; productDesc: string }[] = [];
    availableParts: string[] = [];

    constructor(
        private fb: FormBuilder,
        private utilityService: UtilityService,
        private dialogRef: MatDialogRef<PartDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { mode: 'add' | 'edit'; part?: PartsDetails }
    )
    {
        this.mode = data.mode;
        this.productSummaries = this.utilityService.getProductSummaries();

        this.partForm = this.fb.group({
            partsId: [data.part?.partsId || '', []], 
            partsDesc: [data.part?.partsDesc || '', Validators.required],
            quantity: [data.part?.quantity || 1, [Validators.required, Validators.min(1)]],
            productId: [data.part?.productId || '', Validators.required],
            imageBase64: [data.part?.imageBase64 || '']
        });

        // Initialize available parts if editing
        if (data.part?.productId) {
            this.updateAvailableParts(data.part.productId);
        }

        // React to productId changes
        this.partForm.get('productId')?.valueChanges.subscribe(productId => {
            this.updateAvailableParts(productId);
        });
    }

    updateAvailableParts(productId: string) {
        switch (productId) {
            case 'IAF01': 
                this.availableParts = Object.values(CarParts);
                break;
            case 'IAF02': 
                this.availableParts = Object.values(CellPhoneParts);
                break;
            case 'IAF03': 
                this.availableParts = Object.values(DroneParts);
                break;
            default:
                this.availableParts = [];
        }
    }

    save() {
        if (this.partForm.valid) {
            this.dialogRef.close(this.partForm.value);
        }
    }

    cancel() {
        this.dialogRef.close();
    }

    onFileSelected(event: any) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = () => {
                const base64String = (reader.result as string);

                // Strip "data:image/...;base64," prefix
                const cleanBase64 = base64String.split(',')[1];

                this.partForm.patchValue({ imageBase64: cleanBase64 });
            };
            reader.readAsDataURL(file);
        }
    }
}