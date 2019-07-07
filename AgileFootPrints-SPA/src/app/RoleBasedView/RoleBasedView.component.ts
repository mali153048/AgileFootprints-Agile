import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ProjectService } from '../_services/project.service';

@Component({
  selector: 'app-rolebasedview',
  templateUrl: './RoleBasedView.component.html',
  styleUrls: ['./RoleBasedView.component.css']
})
export class RoleBasedViewComponent implements OnInit {
  @Input() projectId: string;
  /*  @Input() userId: string; */
  @Output() toggleEvent = new EventEmitter<boolean>();
  constructor(private projectService: ProjectService) {}

  ngOnInit() {}

  toggleProject() {
    this.toggleEvent.emit(false);
  }

  getProject() {
    this.projectService.getProject(this.projectId).subscribe(
      next => {
        console.log(next);
      },
      error => {
        console.log(error.message);
      }
    );
  }
}
