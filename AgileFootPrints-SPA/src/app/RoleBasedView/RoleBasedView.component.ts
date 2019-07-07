import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ProjectService } from '../_services/project.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MatTabChangeEvent } from '@angular/material';

@Component({
  selector: 'app-rolebasedview',
  templateUrl: './RoleBasedView.component.html',
  styleUrls: ['./RoleBasedView.component.css']
})
export class RoleBasedViewComponent implements OnInit {
  @Input() projectId: string;
  projectInfo: any = {};
  projectStories = [];
  userId: string;
  userRoles = [];
  isProductOwner = false;
  isScrumMaster = false;
  isDeveloper = false;
  tabIndex: number;

  @Output() toggleEvent = new EventEmitter<boolean>();
  constructor(
    private projectService: ProjectService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.userId = this.authService.decodedToken.nameid;
    this.getProject();
    this.getUserScrumRoles();
  }

  toggleProject() {
    this.toggleEvent.emit(false);
  }

  getProject() {
    this.projectService.getProject(this.projectId).subscribe(
      next => {
        this.projectInfo = next;
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }

  getUserScrumRoles() {
    this.projectService
      .getUserScrumRoles(this.userId, this.projectId)
      .subscribe(
        next => {
          this.userRoles = next;
          this.userRoles.forEach(role => {
            if (role.scrumRolesId === 1) {
              this.isProductOwner = true;
            }
            if (role.scrumRolesId === 2) {
              this.isScrumMaster = true;
            }
            if (role.scrumRolesId === 3) {
              this.isDeveloper = true;
            }
          });
        },
        error => {
          this.alertify.error(error.message);
        }
      );
  }

  tabChanged = (tabChangeEvent: MatTabChangeEvent): void => {
    console.log('tabChangeEvent => ', tabChangeEvent);
    console.log('index => ', tabChangeEvent.index);
    this.tabIndex = tabChangeEvent.index;
    if (this.tabIndex === 0) {
      this.getProject();
      this.getUserScrumRoles();
    } else if (this.tabIndex === 1) {
      // call stories
      this.projectService.getProjectStories(this.projectId).subscribe(
        next => {
          console.log(next);
          this.projectStories = next;
        },
        error => {
          this.alertify.error(error.message);
        }
      );
    } else if (this.tabIndex === 2) {
      // call sprints
    }
  };
}
