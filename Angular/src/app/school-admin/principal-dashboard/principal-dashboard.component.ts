import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatDrawerMode } from '@angular/material/sidenav';
import * as Highcharts from 'highcharts';
import { Subscription } from 'rxjs';
import { TitleService } from 'src/app/shared/services/title.service';

@Component({
  selector: 'app-principal-dashboard',
  templateUrl: './principal-dashboard.component.html',
  styleUrls: ['./principal-dashboard.component.css']
})
export class PrincipalDashboardComponent implements OnInit, OnDestroy {

  openState! : boolean;
  drawerMode!: MatDrawerMode;
  mediaSub!: Subscription;
  activeClass: string = "";

  highcharts = Highcharts;
  chart3Options: Highcharts.Options ={};
  chart2Options: Highcharts.Options = {};
  chartOptions: Highcharts.Options ={};

  constructor(private titleService: TitleService, private mediaObserver: MediaObserver) { }

  ngOnInit(): void {
    this.titleService.setTitle("Dashboard");
    this.mediaSub = this.mediaObserver.media$.subscribe((result:MediaChange)=> {
      if(result.mqAlias !== "xs" && result.mqAlias !== "sm"){
        this.openState = true;
        this.drawerMode = "side";
      }
      else{
        this.openState = false;
        this.drawerMode = "over";
      }

      switch(result.mqAlias){
        case "md":
          this.activeClass = "button-md";
          break;
        case "sm":
          this.activeClass = "button-sm";
          break;
        case "xs":
         this.activeClass = "button-xs";
         break;
        default:
          this.activeClass = "button";
          break;
      }
      
    });

    this.initChar();
  }
  ngOnDestroy(): void{

  }

  initChar(): void{
    
    this.chart2Options  = {
      chart: {
        type: 'line'
    },
    title: {
        text: "Student's performance"
    },
    subtitle: {
        text: ''
    },
    credits:{
      enabled: false
    },
    xAxis: {
      categories: ["2021", "2022", "2023", "2024"]
    },
    yAxis: {
        title: {
            text: ''
        }
      },
    
   
    series: [{
      type: 'area',
        name: 'Payments',
        data: [
            21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950, 10871, 10824,
            10577, 10527
        ]
    }
   ]

    };

    this.chart3Options = {
      chart: {
        type: 'pie',
        options3d: {
            enabled: true,
            alpha: 45
        }
    },
    title: {
        text: 'Students'
    },
    subtitle: {
        text: 'Males/Females'
    },
    plotOptions: {
        pie: {
            innerSize: 100,
            depth: 45
        }
    },
    credits:{
      enabled: false
    },
    series: [{
      type: 'pie',
        name: 'Students',
        data: [
            ['Males', 8],
            ['Females', 3],
           
        ]
    }]
  }

    setTimeout(()=> {
      window.dispatchEvent(new Event('resize'))
    })
  }
}
