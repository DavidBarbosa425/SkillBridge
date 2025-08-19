import { Component, inject, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.less',
})
export class Dashboard implements OnInit {
  aqui() {
    let abc = {};
    this.dashboardService.register(abc).subscribe();
  }
  private dashboardService = inject(DashboardService);

  ngOnInit(): void {}
}
