import { Component, OnInit, Inject } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EditStoryComponent } from '../EditStory/EditStory.component';
import { EpicService } from '../_services/epic.service';
import { PriorityService } from '../_services/priority.service';
import { SprintService } from '../_services/sprint.service';
import { ContributorService } from '../_services/contributor.service';

@Component({
  selector: 'app-rolebasedstoryedit',
  templateUrl: './RoleBasedStoryEdit.component.html',
  styleUrls: ['./RoleBasedStoryEdit.component.css']
})
export class RoleBasedStoryEditComponent implements OnInit {
  story: any = {};
  projectEpics = [];
  projectSprints = [];

  priorities = [];
  contributors = [];
  projectId: string;
  updatedStroy: any = {};
  constructor(
    private contributorService: ContributorService,
    private alertify: AlertifyService,
    private epicService: EpicService,
    private sprintService: SprintService,
    private priorityService: PriorityService,
    public dialogRef: MatDialogRef<RoleBasedStoryEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.story = this.data.storyDetails;
    this.projectId = this.data.projectId;
    this.getEpics();
    this.getPriorities();
    this.getSprints();
    this.getContributors();
    console.log(this.projectEpics);
  }
  getEpics() {
    this.epicService.getProjectEpics(this.projectId).subscribe(
      next => {
        this.projectEpics = next[0].epics;
        console.log('Epics fetched', this.projectEpics);
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
  getSprints() {
    this.sprintService.getSprints(this.projectId).subscribe(
      next => {
        this.projectSprints = next;
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
  getContributors() {
    this.contributorService.getprojectContributors(this.projectId).subscribe(
      next => {
        this.contributors = next;
        console.log(this.contributors);
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
  getPriorities() {
    this.priorityService.getPriorities().subscribe(
      next => {
        this.priorities = next;
      },
      error => {
        this.alertify.error(error.message);
      }
    );
  }
  save() {
    console.log('Helloo Ali', this.story);
    this.dialogRef.close(this.story);
  }
  close() {
    this.dialogRef.close(null);
  }
}
