//Temă lab 8
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Teacher } from "../data/interfaces/teacher";
import { Manual } from "../data/interfaces/manual";
import { TeacherJoinedManual } from "../data/interfaces/teacherjoinedmanual";
import { AddTeacher } from "../data/interfaces/addteacher";
import { MatCardModule } from "@angular/material/card";
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-demo-request',
  standalone: true,
  imports: [CommonModule, MatCardModule, HttpClientModule, FormsModule],
  templateUrl: './demo-request.component.html',
  styleUrls: ['./demo-request.component.scss']
})
export class DemoRequestComponent implements OnInit {

  public teacher: Teacher[] = [];
  public manual: Manual[] = [];
  public teacherManualMap: Map<string, string[]> = new Map();
  public newTeacher: AddTeacher = { name: "", salary: 0 };
  public updateTeacher: Teacher = { name: "", salary: 0, id: 0 };
  private readonly apiUrl = "https://localhost:7166/api";

  constructor(private readonly httpClient: HttpClient) {
  }

  ngOnInit(): void {
    this.httpClient.get<Teacher[]>(`${this.apiUrl}/Database/Teacher`).subscribe(
      (teachers) => {
        console.log(teachers);
        this.teacher.push(...teachers);
      },
      (error) => {
        console.error('There was an error!', error);
      }
    );

    this.httpClient.get<Manual[]>(`${this.apiUrl}/Database/Manual`).subscribe(
      (manuals) => {
        console.log(manuals);
        this.manual.push(...manuals);
      },
      (error) => {
        console.error('There was an error!', error);
      }
    );

    this.httpClient.get<TeacherJoinedManual[]>(`${this.apiUrl}/Database/TeacherManualJoin`).subscribe(
      (teacherJoinedManuals) => {
        teacherJoinedManuals.forEach(tm => {
          // Verifică dacă profesorul există deja în map
          if (!this.teacherManualMap.has(tm.teacherName)) {
            // Dacă profesorul nu există, inițializează un array gol pentru manuale
            this.teacherManualMap.set(tm.teacherName, []);
          }
          // Adaugă numele manualului în array-ul de manuale al profesorului
          this.teacherManualMap.get(tm.teacherName)?.push(tm.manualName);
        });
      },
      (error) => {
        console.error('There was an error!', error);
      }
    );
  }

  addTeacher() {
    this.httpClient.post(`${this.apiUrl}/Database/Teacher`, this.newTeacher).subscribe(
      (response) => {
        console.log('Profesor adăugat cu succes:', response);
        this.newTeacher = { name: "", salary: 0 };
      },
      (error) => {
        console.error('A apărut o eroare la adăugarea profesorului:', error);
      }
    );
  }

  updateTeacherData() {
    this.httpClient.put(`${this.apiUrl}/Database/Teacher_update`, this.updateTeacher).subscribe(
      (response) => {
        console.log('Datele profesorului au fost actualizate cu succes:', response);
        this.updateTeacher = { name: "", salary: 0, id: 0 };
      },
      (error) => {
        console.error('A apărut o eroare la actualizarea datelor profesorului:', error);
      }
    );
  }
}
