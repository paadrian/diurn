import { Component } from '@angular/core';
import { ActivityService } from '../services/activity.service';

@Component({
  selector: 'app-activity',
  imports: [],
  templateUrl: './activity.detail.component.html',
  styleUrl: './activity.detail.component.scss'
})
export class ActivityDetailComponent {
  constructor(activityService: ActivityService)
  {

  }
}
