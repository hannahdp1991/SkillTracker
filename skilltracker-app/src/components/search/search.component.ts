import { Component, OnInit } from "@angular/core";
import { AbstractControl, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Observable } from "rxjs";
import { SkillProfile } from "src/models/skill-profile";
import { SkillTrackerService } from "src/services/skill-tracker.service";

@Component({ templateUrl: "./search.component.html" })
export class SearchComponent implements OnInit {
    searchForm!: FormGroup;
    skillProfileList$!: Observable<Array<SkillProfile>>;

    get criteriaTypeControl(): AbstractControl | null {
        return this.searchForm.get("criteriaType");
    }

    get criteriaValueControl(): AbstractControl | null {
        return this.searchForm.get("criteriaValue");
    }

    constructor(private readonly formBuilder: FormBuilder,
        private readonly skillService: SkillTrackerService) {
    }

    public ngOnInit(): void {
        this.searchForm = this.formBuilder.group({
            criteriaType: ["", Validators.required],
            criteriaValue: ["", Validators.required]
        });
    }

    public onSubmit(): void {
        if (this.searchForm.valid) {
            this.skillProfileList$ = this.skillService.getSkillsByCriteria(this.searchForm.get("criteriaType")?.value,
                this.searchForm.get("criteriaValue")?.value);
        }
    }
}