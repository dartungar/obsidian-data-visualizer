import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataLoaderFormComponent } from './data-loader-form.component';

describe('DataLoaderFormComponent', () => {
  let component: DataLoaderFormComponent;
  let fixture: ComponentFixture<DataLoaderFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataLoaderFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataLoaderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
