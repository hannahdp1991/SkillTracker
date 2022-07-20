import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { SearchComponent } from "src/components/search/search.component";
import { SkillProfileListViewer } from "src/components/skilllistviewer/skill-list-viewer.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: SkillProfileListViewer
      },
      {
        path: "skills",
        component: SkillProfileListViewer
      },
      {
        path: "search",
        component: SearchComponent
      }
    ]
  },
  {
    path: "**",
    redirectTo: "",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
