import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule} from '@angular/router';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatCardModule} from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatMenuModule} from '@angular/material/menu';
import {MatListModule} from '@angular/material/list';
import {MatDividerModule} from '@angular/material/divider';
import { HighchartsChartModule } from 'highcharts-angular';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatStepperModule} from '@angular/material/stepper';
import {MatSelectModule} from '@angular/material/select';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';




import { FlexLayoutModule } from '@angular/flex-layout';

import { HeaderComponent } from '../components/header/header.component';
import { AlertModule } from 'ngx-alerts';
import { MatDialogModule } from '@angular/material/dialog';
import { ResultTableComponent } from '../components/result-table/result-table.component';
import { ResultListComponent } from '../components/result-list/result-list.component';
import { ResultViewComponent } from '../components/result-view/result-view.component';






@NgModule({
  declarations: [HeaderComponent,ResultTableComponent,  ResultListComponent, ResultViewComponent],
  imports: [
    AlertModule.forRoot({maxMessages: 5, timeout: 5000, positionX: 'right'}),
    CommonModule,
    MatSelectModule,
    FormsModule, 
    ReactiveFormsModule,
    RouterModule,
    MatStepperModule,
    MatExpansionModule,
    HighchartsChartModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    FlexLayoutModule,
    MatCardModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatMenuModule,
    MatListModule,
    MatTableModule,
    MatDividerModule,
    MatDialogModule,
    MatPaginatorModule
  ],

  exports:[ 
    AlertModule,
    FormsModule, 
    MatSelectModule,
    ReactiveFormsModule,
    HighchartsChartModule,
    MatButtonModule,
    MatExpansionModule,
    MatStepperModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    FlexLayoutModule,
    MatCardModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatMenuModule,
    MatListModule,
    MatTableModule,
    HeaderComponent,
    ResultTableComponent,
    ResultListComponent,
    ResultViewComponent,
    MatDividerModule,
    MatDialogModule,
    MatPaginatorModule
    ]

})
export class SharedModuleModule { }
