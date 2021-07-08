import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatDrawerMode } from '@angular/material/sidenav';
import { Subscription } from 'rxjs';
import { TitleService } from 'src/app/shared/services/title.service';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {

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
    this.chartOptions  = {
      chart: {
        type: 'line'
    },
    title: {
        text: 'User Engagement'
    },
    subtitle: {
        text: ''
    },
    credits:{
      enabled: false
    },
    xAxis: {
      categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
    },
    yAxis: {
        title: {
            text: ''
        }
      },
    
   
    series: [{
      type: 'line',
        name: 'Teachers',
        data: [
            21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950, 10871, 10824,
            10577, 10527
        ]
    }, {
         type: 'line', 
        name: 'Students',
        data: [
            21000, 20000, 19000, 18000, 18000, 17000, 16000, 15537, 14162, 12787,
            12600, 11400]
    }, 
    {
      type: 'line', 
     name: 'Parents',
     data: [
         5893, 8453, 9538, 18342, 45424, 352445, 93758, 15537, 14162, 12787,
         12600, 11400]
 }]

    };

    this.chart2Options  = {
      chart: {
        type: 'line'
    },
    title: {
        text: 'Revenue Report'
    },
    subtitle: {
        text: ''
    },
    credits:{
      enabled: false
    },
    xAxis: {
      categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
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
        text: 'Payments'
    },
    subtitle: {
        text: 'Payments/Debts'
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
        name: 'Payments',
        data: [
            ['Total Paid', 8],
            ['Total Unpaid', 3],
           
        ]
    }]
  }

    setTimeout(()=> {
      window.dispatchEvent(new Event('resize'))
    })
  }
}
