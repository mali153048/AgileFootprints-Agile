import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  baseUrl = environment.apiUrl + 'meeting/';
  constructor(private http: HttpClient) {}

  createMeeting(meetingModel: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'createMeeting', meetingModel);
  }

  getProjectMeetings(projectId: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'getProjectMeetings/' + projectId);
  }

  delete(meetingId: string): Observable<any> {
    return this.http.delete(this.baseUrl + 'delete/' + meetingId);
  }
  editMeeting(meetingModel: any, meetingId: string) {
    return this.http.patch<any>(
      this.baseUrl + 'edit/' + meetingId,
      meetingModel
    );
  }
}
