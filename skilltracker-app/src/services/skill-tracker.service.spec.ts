import { HttpClient } from "@angular/common/http";
import { of } from "rxjs";
import { SkillProfile } from "src/models/skill-profile";
import { SkillTrackerService } from "./skill-tracker.service";

describe("Skill tracker service", () => {
    var service: SkillTrackerService;
    var mockHttpClient: HttpClient;
    
    const skillProfile1 = new SkillProfile();
    const skillProfile2 = new SkillProfile();
    const skillProfile3 = new SkillProfile();

    const skillProfileList = [
        skillProfile1,
        skillProfile2
    ];

    const skillProfileListByCriteria = [
        skillProfile3
    ];

    beforeEach(() => {
        mockHttpClient = jasmine.createSpyObj(HttpClient.name, ["get"]);
        (mockHttpClient.get as jasmine.Spy).and.callFake(arg => {
            if (arg === "http://localhost:9000/adminservice") {
                return of(skillProfileList);
            }
            else {
                return of(skillProfileListByCriteria)
            }
        });

        service = new SkillTrackerService(mockHttpClient);
    });

    describe("getSkills", () => {
        it("should retrieve all skill profiles", () => {
            service.getSkills().subscribe(response => {
                expect(response.length).toEqual(2);
                expect(mockHttpClient.get).toHaveBeenCalledTimes(1);
            });
        });
    });

    describe("getSkillsByCriteria", () => {
        it("should retrieve skills based on search criteria", () => {
            service.getSkillsByCriteria("associateid", "12").subscribe(response => {
                expect(response.length).toEqual(1);
                expect(mockHttpClient.get).toHaveBeenCalledTimes(1);
            });
        });
    });
});