import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ProjectService } from '../_services/project.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import {
  MatTabChangeEvent,
  MatDialogConfig,
  MatDialog
} from '@angular/material';
import { StoryService } from '../_services/story.service';
import { EditStoryComponent } from '../EditStory/EditStory.component';
import { RoleBasedStoryEditComponent } from '../RoleBasedStoryEdit/RoleBasedStoryEdit.component';
import { SprintService } from '../_services/sprint.service';

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
  sprints = [];
  userRoles = [];
  startSprintIds = [];
  endSprintIds = [];
  isProductOwner = false;
  isScrumMaster = false;
  isDeveloper = false;
  tabIndex: number;
  space = ' ';

  @Output() toggleEvent = new EventEmitter<boolean>();
  constructor(
    private projectService: ProjectService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private storyService: StoryService,
    private sprintService: SprintService,
    public dialog: MatDialog
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
      this.getSprints();
    }
    // tslint:disable-next-line:semicolon
  };

  Delete(id: string) {
    this.alertify.confirm(
      'Are you sure you want to delete this story ? The story will be deleted from everywhere. ',
      () => {
        this.storyService.deleteStory(id).subscribe(
          next => {
            const toFilter = this.projectStories.filter(x => x.id === id);
            const index = this.projectStories.indexOf(toFilter);
            this.projectStories.splice(index, 1);
            this.alertify.success('Deleted Success');
          },
          error => {
            this.alertify.error(error.message);
          }
        );
      }
    );
  }
  getProjectStories() {
    this.projectService.getProjectStories(this.projectId).subscribe(
      next => {
        console.log(next);
        this.projectStories = next;
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
  Edit(id: string) {
    this.storyService.getStory(+id).subscribe(next => {
      console.log(next);
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.data = {
        storyDetails: next,
        projectId: this.projectId
      };
      const dialogRef = this.dialog.open(
        RoleBasedStoryEditComponent,
        dialogConfig
      );
      dialogRef.afterClosed().subscribe(result => {
        if (result === null) {
          return;
        }
        this.storyService.editStory(result.id, result).subscribe(
          () => {
            this.getProjectStories();
            this.alertify.success('Story Edited Successfully');
          },
          error => {
            this.alertify.error(error.message);
          }
        );
      });
    });
  }

  getSprints() {
    this.sprintService.getSprints(this.projectId).subscribe(
      next => {
        console.log(next);
        next.forEach(element => {
          element.startDate = new Date(element.startDate);
          element.endDate = new Date(element.endDate);

          if (
            element.startDate.getTime() ===
              new Date('0001-01-01T00:00:00').getTime() ||
            element.endDate.getTime() ===
              new Date('0001-01-01T00:00:00').getTime()
          ) {
            element.startDate = undefined;
            element.endDate = undefined;
          }
          console.log('Sprint is ', element);
          console.log(element.startDate);
          console.log(new Date());
          if (
            new Date(element.startDate).getDate() === new Date().getDate() &&
            element.statusId === 1 // sprint must not be already started
          ) {
            // send element.id
            this.startSprintIds.push(element.id);
          }
          if (new Date(element.endDate).getTime() < new Date().getTime()) {
            this.endSprintIds.push(element.id);
          }

          console.log('yo', this.startSprintIds);
        });
        if (this.startSprintIds.length > 0) {
          // console.log(this.startSprintIds);
          this.sprintService.startNow(this.startSprintIds).subscribe();
        }
        if (this.endSprintIds.length > 0) {
          this.sprintService.endSprint(this.endSprintIds).subscribe();
        }
        this.sprints = next;
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
}
