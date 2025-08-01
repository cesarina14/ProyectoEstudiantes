import { ChangeDetectorRef, Component, effect, inject, OnInit } from "@angular/core";
import { ChartModule } from "primeng/chart";
import { SummaryCalification } from "../../../app/dashboard/model/summary-calification";
import { StudentService } from "../../../Student/service/student.service";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { SummaryService } from "../service/summary.service";
import { HttpClientModule } from "@angular/common/http";
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID, Inject } from '@angular/core'
import { ConfigService } from "../../../config.service";
import { DesignerService } from "../../../designer.service";


@Component({
  selector: 'app-dashboard-component',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [ChartModule,CommonModule,HttpClientModule],
  

})
export class DashboardComponent implements OnInit {
  public Data: any;
  public Options: any;
  public SummaryCalificationList: SummaryCalification[] = [];
  public DateLoaded :boolean =false;
  private platformId = inject(PLATFORM_ID);

  constructor(private _Service: SummaryService,
       private cd: ChangeDetectorRef,
       public configService: ConfigService,
       public designerService: DesignerService,
    ) {}

  ngOnInit(): void {
    this.loadChart(); 
  }
  themeEffect = effect(() => {
    if (this.configService.transitionComplete()) {
        if (this.designerService.preset()) {
            this.loadChart()
        }
    }
});

  private loadChart(): void {
    if (isPlatformBrowser(this.platformId)) {
    this._Service.listCalificationSummary().subscribe((_response) => {
      if (_response.Success) {
        this.SummaryCalificationList = _response.Value;

        const colorMap: { [key: string]: string } = {
          A: '#42A5F5',
          B: '#66BB6A',
          C: '#FFA726',
          D: '#FF7043',
          F: '#AB47BC'
        };

        const labels = this.SummaryCalificationList.map(r => r.Literal);
        const counts = this.SummaryCalificationList.map(r => r.Count);
        const backgroundColors = this.SummaryCalificationList.map(r => colorMap[r.Literal] || '#9E9E9E');

        this.Data = {
          labels,
          datasets: [
            {
              data: counts,
              backgroundColor: backgroundColors,
              hoverBackgroundColor: backgroundColors
            }
          ]
        };

        this.Options = {
          responsive: true,
          plugins: {
            legend: { position: 'bottom' },
            title: {
              display: true,
              text: 'Distribución de Calificaciones'
            }
          }
        };
      }

    });
    this.cd.markForCheck()
  }
  }
}
